using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ApplicationService.Crm.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Crm
{
    public class CompanyService : BaseService<CompanyService>, ICompanyService
    {
        private readonly IRepository<Company> companyRepository;
        private readonly IRepository<CompanyRelationshipType> companyRelationshipTypeRepository;
        private readonly IPhotoService photoService;
        private readonly IRepository<Photo> photoRepository;

        public CompanyService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<Company> companyRepository
            , IRepository<CompanyRelationshipType> companyRelationshipTypeRepository
            , IPhotoService photoService
            , IRepository<Photo> photoRepository) : base(applicationServiceBuilder)
        {
            this.companyRepository = companyRepository;
            this.companyRelationshipTypeRepository = companyRelationshipTypeRepository;
            this.photoService = photoService;
            this.photoRepository = photoRepository;
        }

        public async Task<List<SelectModel>> GetCompanySelectItemsAsync()
        {
            return await Task.Run(() => companyRepository.Where(x => x.IsActive == true).Select(x => new SelectModel { Id = x.Id, Name = x.CompanyName }).ToList());
        }

        public async Task<List<SelectModel>> GetCompanySelectItemsByBPIdAsync(List<int> businessProfileIds)
        {
            return await Task.Run(() => companyRepository
            .Where(x => businessProfileIds.Contains(x.BusinessProfileId) && x.IsActive == true)
            .Select(x => new SelectModel
            {
                Id = x.Id,
                Name = x.CompanyName
            }).ToList());
        }

        public async Task<List<SelectModel>> GetCompanySelectItemsByBPIdAsync(int businessProfileId)
        {
            if (businessProfileId == 0)
            {
                businessProfileId = this.LoggedInUser.DefaultBusinessProfileId;
            }

            return await Task.Run(() => companyRepository
            .Where(x => x.BusinessProfileId == businessProfileId && x.IsActive == true)
            .Select(x => new SelectModel
            {
                Id = x.Id,
                Name = x.CompanyName
            }).ToList());
        }

        public async Task<LoadResult> GetCompanyListAsync(DataSourceLoadOptionsBase options)
        {
            IQueryable<Company> usersQuery = companyRepository.AsQueryable();

            if (!LoggedInUser.IsSuperAdmin)
            {
                int businessProfileId = LoggedInUser.DefaultBusinessProfileId;
                usersQuery = usersQuery.Where(x => x.BusinessProfileId == businessProfileId);
            }
            options.Select = new[] { "Id", "CompanyName", "AccountNumber", "ABN", "ACN", "MainPhone", "MobilePhone", "Email", "Website", "IsArchived", "IsActive" };

            return await Task.Run(() => DataSourceLoader.Load(usersQuery, options));
        }

        public async Task<CompanyModel> GetCompanyByIdAsync(int id)
        {
            if (id == 0)
            {
                return new CompanyModel { BusinessProfileId = LoggedInUser.DefaultBusinessProfileId };
            }

            Company company = await companyRepository.Where(x => x.Id == id).Include(x => x.CompanyRelationshipTypes).Include(x => x.Logo).FirstOrDefaultAsync();
            if (company.Logo == null)
            {
                company.Logo = new Photo();
            }

            if (company == null)
            {
                throw new SoftcodeNotFoundException("Company not found");
            }

            CompanyModel companyModel = Mapper.Map<CompanyModel>(company);

            companyModel.RelationshipTypes = company.CompanyRelationshipTypes.Select(x => x.RelationshipTypeId).ToList();

            return companyModel;
        }

        public async Task<int> SaveCompanyDetailsAsync(int id, CompanyModel companyModel)
        {
            if (companyModel == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid company data");
            }

            Company company = new Company();
            if (!companyModel.Logo.IsUpdated && !companyModel.Logo.IsDeleted)
            {
                companyModel.Logo = null;
            }

            if (companyModel.Id > 0)
            {
                if (companyModel.Logo != null)
                {
                    company = await companyRepository.Where(x => x.Id == id).Include(x => x.CompanyRelationshipTypes).Include(x => x.Logo).FirstOrDefaultAsync();
                }
                else
                {
                    company = await companyRepository.Where(x => x.Id == id).Include(x => x.CompanyRelationshipTypes).FirstOrDefaultAsync();
                }

                if (company == null)
                {
                    throw new SoftcodeNotFoundException("Company not found for edit");
                }


                if (company.CompanyRelationshipTypes.Count > 0)
                {
                    companyRelationshipTypeRepository.Remove(company.CompanyRelationshipTypes.ToList());
                }
            }

            Mapper.Map(companyModel, company);


            foreach (int item in companyModel.RelationshipTypes)
            {
                company.CompanyRelationshipTypes.Add(new CompanyRelationshipType { RelationshipTypeId = item });
            }

            if (companyModel.LogoId > 0 && companyModel.Logo != null)
            {
                if (companyModel.Logo.IsDeleted)
                {
                    company.LogoId = null;
                    company.Logo = null;
                }
                else
                {
                    company.Logo.OrginalFileName = companyModel.Logo.UploadedFileName;
                    company.Logo.PhotoThumb = this.photoService.GetImageFile(companyModel.Logo.UploadedFileName);
                }
            }
            else if (!string.IsNullOrEmpty(companyModel.Logo?.UploadedFileName))
            {
                Photo photo = new Photo();
                photo.PhotoThumb = this.photoService.GetImageFile(companyModel.Logo.UploadedFileName);
                photo.FileName = companyModel.CompanyName;
                photo.OrginalFileName = companyModel.Logo.UploadedFileName;
                photo.IsDefault = true;
                photo.IsVisibleInPublicPortal = true;
                photo.CreatedDateTime = DateTime.UtcNow;
                photo.CreatedByContactId = LoggedInUser.ContectId;
                company.Logo = photo;
            }

            if (id > 0)
            {
                //Delete Photo
                if (companyModel.Logo != null && companyModel.Logo.IsDeleted)
                {
                    photoRepository.Remove(company.Logo);
                }

                return companyRepository.UpdateAsync(company).Result.Id;
            }
            return companyRepository.CreateAsync(company).Result.Id;

        }

        public async Task<int> DeleteCompanyAsync(int id)
        {
            Company company = companyRepository.Where(x => x.Id == id).Include(x => x.CompanyRelationshipTypes).FirstOrDefault();

            if (company == null)
            {
                throw new SoftcodeNotFoundException("Company not found");
            }

            if (company.CompanyRelationshipTypes.Count > 0)
            {
                companyRelationshipTypeRepository.Remove(company.CompanyRelationshipTypes.ToList());
            }

            return await companyRepository.DeleteAsync(company);
        }

        public async Task<bool> DeleteCompaniesAsync(List<int> ids)
        {
            if (ids == null)
            {
                throw new SoftcodeArgumentMissingException("Company not found");
            }

            if (!await companyRepository.ExistsAsync(x => ids.Contains(x.Id)))
            {
                throw new SoftcodeArgumentMissingException("Company not found");
            }

            return await companyRepository.DeleteAsync(t => ids.Contains(t.Id)) > 0;
        }

        //public async Task<LoadResult> GetCompanyListByBPIdAsync(int businessProfileId, DataSourceLoadOptionsBase options)
        //{
        //    options.Select = new[] { "Id", "CompanyName" };
        //    return await this.companyRepository.GetDevExpressListAsync(options, t=>t.BusinessProfileId == businessProfileId && t.IsActive == true);
        //}

        /// <summary>
        /// Get entity detail tab list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TabModel> GetCompanyDetailsTabs(int id)
        {
            List<TabModel> tabModels = new List<TabModel>
            {
                new TabModel(1, "General Information", "generalInformation")
            };

            if (id == 0)
            {
                return tabModels;
            }

            tabModels.Add(new TabModel(2, "Contact", "contact"));
            tabModels.Add(new TabModel(3, "Addresses", "addresses"));
            tabModels.Add(new TabModel(4, "Documents", "documents"));
            tabModels.Add(new TabModel(5, "Communications ", "communications"));

            //tabModels.Add(new TabModel(3, "Documents", "documents"));
            //tabModels.Add(new TabModel(4, "Communications", "communications"));
            //tabModels.Add(new TabModel(5, "Compliances", "compliances"));

            return tabModels;
        }

        public Task<bool> SendEmailAsync()
        {
            throw new NotImplementedException();

            //var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
        }
    }
}
