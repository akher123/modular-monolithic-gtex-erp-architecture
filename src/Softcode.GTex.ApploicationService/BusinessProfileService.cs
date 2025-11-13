using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softcode.GTex.ExceptionHelper;

namespace Softcode.GTex.ApploicationService
{
    public class BusinessProfileService : BaseService<BusinessProfileService>, IBusinessProfileService
    {
        private readonly IRepository<BusinessProfile> businessProfileRepository;
        private readonly IRepository<Photo> photoRepository;

        private readonly IPhotoService photoService;

        public BusinessProfileService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<BusinessProfile> businessProfileRepository
            , IRepository<Photo> photoRepository
            , IPhotoService photoService) : base(applicationServiceBuilder)
        {
            this.businessProfileRepository = businessProfileRepository;
            this.photoRepository = photoRepository;
            this.photoService = photoService;
        }

        public async Task<LoadResult> GetBusinessProfileListAsync(DataSourceLoadOptionsBase options)
        {
            IQueryable<BusinessProfile> bpQuery = businessProfileRepository.AsQueryable();

            if (!LoggedInUser.IsDefaultBusinessProfile)
            {
                int businessProfileId = LoggedInUser.DefaultBusinessProfileId;
                bpQuery = bpQuery.Where(x => x.Id == businessProfileId);
            }

            options.Select = new[] { "Id", "CompanyName", "Number", "ABN", "ACN", "Phone", "Fax", "Mobile", "Email", "Website", "IsDefault", "IsActive" };
            return await Task.Run(() => DataSourceLoader.Load(bpQuery, options));
        }

        public async Task<BusinessProfileModel> GetBusinessProfileModelByIdAsync(int id)
        {
            if (id == 0)
            {
                return new BusinessProfileModel()
                {
                    CompId = GetNewCompId()
                };
            }

            BusinessProfile businessProfile = null;
            if (this.LoggedInUser.IsDefaultBusinessProfile)
            {
                businessProfile = await businessProfileRepository.Where(x => x.Id == id).Include(x => x.Logo).FirstOrDefaultAsync();
            }
            else
            {
                int loggedInUserBusinessProfileId = this.LoggedInUser.DefaultBusinessProfileId;
                businessProfile = await businessProfileRepository.Where(x => x.Id == id && x.Id == loggedInUserBusinessProfileId).Include(x => x.Logo).FirstOrDefaultAsync();
            }

            if (businessProfile == null)
            {
                throw new SoftcodeNotFoundException("Business profile not found");
            }


            if (businessProfile.Logo == null) businessProfile.Logo = new Photo();

            return Mapper.Map<BusinessProfileModel>(businessProfile);
        }

        public async Task<List<SelectModel>> GetUserBusinessProfileSelectItemsAsync()
        {
            if (LoggedInUser.IsSuperAdmin)
            {

                return await Task.Run(() => businessProfileRepository.Where(x => x.IsActive)
                    .Select(x => new SelectModel
                    {
                        Id = x.Id,
                        Name = x.CompanyName,
                        IsDefault = x.IsDefault
                    }).ToList());
            }
            else
            {
                int[] bpIds = LoggedInUser.UserBusinessProfileIds;
                return await Task.Run(() => businessProfileRepository.Where(x => x.IsActive && bpIds.Contains(x.Id))
                                 .Select(x => new SelectModel
                                 {
                                     Id = x.Id,
                                     Name = x.CompanyName,
                                     IsDefault = x.IsDefault
                                 }).ToList());
            }
        }

        public async Task<List<SelectModel>> GetContactBusinessProfileSelectItemsByUserBPIdsAsync()
        {
            if (LoggedInUser.IsSuperAdmin)
            {

                return await Task.Run(() => businessProfileRepository.Where(x => x.IsActive)
                    .Select(x => new SelectModel
                    {
                        Id = x.Id,
                        Name = x.CompanyName,
                        Tag=x.DomainName,
                        IsDefault = x.IsDefault
                    }).ToList());
            }
            else
            {
                int[] bpIds = LoggedInUser.ContactBusinessProfileIds.Intersect(LoggedInUser.UserBusinessProfileIds).ToArray();
                return await Task.Run(() => businessProfileRepository.Where(x => x.IsActive && bpIds.Contains(x.Id))
                                 .Select(x => new SelectModel
                                 {
                                     Id = x.Id,
                                     Name = x.CompanyName,
                                     Tag = x.DomainName,
                                     IsDefault = x.IsDefault
                                 }).ToList());
            }
        }
        public List<TabModel> GetBusinessProfileDetailsTabs(int id)
        {
            List<TabModel> tabModels = new List<TabModel>
            {
                new TabModel(1, "General Information", "generalInformation")
            };

            if (id == 0)
            {
                return tabModels;
            }

            tabModels.Add(new TabModel(2, "Address", "addresses"));
            //tabModels.Add(new TabModel(3, "Compliance", "compliances"));
            //tabModels.Add(new TabModel(4, "Sites", "sites"));
            //tabModels.Add(new TabModel(5, "Departments", "departments"));
            //tabModels.Add(new TabModel(6, "Cost Centres", "costCentres"));
            //tabModels.Add(new TabModel(7, "Business Units", "businessUnits"));
            //tabModels.Add(new TabModel(8, "Bank Accounts", "bankAccounts"));
            //tabModels.Add(new TabModel(9, "GL Accounts", "glAccounts"));

            return tabModels;
        }

        public async Task<bool> DeleteBusinessProfilesAsync(List<int> ids)
        {
            if (ids == null)
            {
                throw new SoftcodeArgumentMissingException("Business Profile not found");
            }

            if (!await businessProfileRepository.ExistsAsync(x => ids.Contains(x.Id)))
            {
                throw new SoftcodeArgumentMissingException("Business Profile not found");
            }

            return await businessProfileRepository.DeleteAsync(t => ids.Contains(t.Id)) > 0;
        }

        public async Task<bool> DeleteBusinessProfileByIdAsync(int id)
        {
            if (id < 1)
            {
                throw new SoftcodeArgumentMissingException("Business Profile not found");
            }

            if (!await businessProfileRepository.ExistsAsync(x => x.Id == id))
            {
                throw new SoftcodeArgumentMissingException("Business Profile not found");
            }

            return await businessProfileRepository.DeleteAsync(t => t.Id == id) > 0;
        }


        public async Task<int> SaveBusinessProfileAsync(BusinessProfileModel businessProfileModel)
        {
            if (businessProfileModel == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid business profile");
            }

            BusinessProfile dbBP = new BusinessProfile();

            if (!businessProfileModel.Logo.IsUpdated && !businessProfileModel.Logo.IsDeleted)
            {
                businessProfileModel.Logo = null;
            }

            if (businessProfileModel.Id > 0)
            {
                if (businessProfileModel.Logo != null)
                {
                    dbBP = businessProfileRepository.Where(b => b.Id == businessProfileModel.Id).Include(p => p.Logo).FirstOrDefault();
                }
                else
                {
                    dbBP = businessProfileRepository.Where(b => b.Id == businessProfileModel.Id).FirstOrDefault();
                }

                if (dbBP == null)
                {
                    throw new SoftcodeNotFoundException("Business profile not found for edit");
                }
            }
            else
            {
                dbBP.CompId = GetNewCompId();
            }


            if (dbBP != null)
            {
                Mapper.Map(businessProfileModel, dbBP);

                if (businessProfileModel.IsDefault)
                {
                    dbBP.IsActive = true;
                    //set default = false for previous default record 
                    businessProfileRepository.Attach(businessProfileRepository
                                                    .Where(x => x.Id != businessProfileModel.Id && x.IsDefault).ToList()
                                                    .Select(x => { x.IsDefault = false; return x; }).FirstOrDefault());
                }


                if (businessProfileModel.LogoId > 0 && businessProfileModel.Logo != null)
                {
                    if (businessProfileModel.Logo.IsDeleted)
                    {
                        dbBP.LogoId = null;
                        dbBP.Logo = null;
                    }
                    else
                    {
                        dbBP.Logo.OrginalFileName = businessProfileModel.Logo.UploadedFileName;
                        dbBP.Logo.PhotoThumb = this.photoService.GetImageFile(businessProfileModel.Logo.UploadedFileName);
                    }
                }
                else if (!string.IsNullOrEmpty(businessProfileModel.Logo?.UploadedFileName))
                {
                    Photo photo = new Photo();
                    photo.PhotoThumb = this.photoService.GetImageFile(businessProfileModel.Logo.UploadedFileName);
                    photo.FileName = businessProfileModel.CompanyName;
                    photo.OrginalFileName = businessProfileModel.Logo.UploadedFileName;
                    photo.IsDefault = true;
                    photo.IsVisibleInPublicPortal = true;
                    photo.CreatedDateTime = DateTime.UtcNow;
                    photo.CreatedByContactId = LoggedInUser.ContectId;
                    dbBP.Logo = photo;
                }



                if (businessProfileModel.Id == 0)
                {
                    await businessProfileRepository.CreateAsync(dbBP);
                }
                else
                {
                    await businessProfileRepository.UpdateAsync(dbBP);
                    //Delete logo
                    if (businessProfileModel.Logo != null && businessProfileModel.Logo.IsDeleted)
                    {
                        photoRepository.Delete(p => p.Id == businessProfileModel.Logo.Id);
                    }
                }

            }
            return dbBP.Id;
        }

        private string GetNewCompId()
        {
            string maxCompId =businessProfileRepository
                .AsQueryable()
                .Max(x => x.CompId);
           return maxCompId.PadingWithIncrement(3);
        }
    }
}
