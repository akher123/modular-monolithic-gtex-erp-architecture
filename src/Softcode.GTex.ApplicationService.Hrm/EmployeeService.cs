using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ApplicationService.Hrm.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Hrm
{
    public class EmployeeService : BaseService<EmployeeService>, IEmployeeService
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<Contact> contactRepository;
        private readonly IRepository<ContactSpecialisation> contactSpecialisationRepository;
        private readonly IRepository<EntityContact> entityContactRepository;
        private readonly IRepository<ContactBusinessProfile> contactBusinessProfileRepository;
        private readonly IRepository<EmployeeCostCentre> employeeCostCentreRepository;
        private readonly IRepository<EmployeeSite> employeeSiteRepository;
        private readonly IRepository<Photo> photoRepository;
        private readonly IPhotoService photoService;
        public EmployeeService(IApplicationServiceBuilder applicationServiceBuilder,
            IRepository<Employee> employeeRepository,
            IRepository<EmployeeCostCentre> employeeCostCentreRepository,
            IRepository<EmployeeSite> employeeSiteRepository,
            IRepository<Contact> contactRepository,
            IRepository<ContactSpecialisation> contactSpecialisationRepository,
            IRepository<EntityContact> entityContactRepository,
            IRepository<ContactBusinessProfile> contactBusinessProfileRepository,
            IRepository<Photo> photoRepository,
            IPhotoService photoService) : base(applicationServiceBuilder)
        {
            this.employeeRepository = employeeRepository;
            this.employeeCostCentreRepository = employeeCostCentreRepository;
            this.employeeSiteRepository = employeeSiteRepository;
            this.contactRepository = contactRepository;
            this.contactSpecialisationRepository = contactSpecialisationRepository;
            this.entityContactRepository = entityContactRepository;
            this.contactBusinessProfileRepository = contactBusinessProfileRepository;
            this.photoRepository = photoRepository;
            this.photoService = photoService;
        }

        public async Task<LoadResult> GetEmployeeListAsync(DataSourceLoadOptionsBase options)
        {
            int loggedInUserBPId = this.LoggedInUser.DefaultBusinessProfileId;
            bool isDefaultBusinessProfileUser = this.LoggedInUser.IsDefaultBusinessProfile;
            var employee = this.contactRepository
                      .Where(c => c.ContactType == Configuration.ApplicationContactType.Employee);

            if (!this.LoggedInUser.IsSuperAdmin) {
                employee = employee.Where(c => (c.ContactBusinessProfiles.Any(cbp => cbp.BusinessProfileId == loggedInUserBPId)));
            }

            var query = employee
                     .Include(t => t.Employee)
                     .Include(t => t.Postion)
                     .Include(t => t.ContactBusinessProfiles).Include(t => t.EntityContacts)
                     .Include(t => t.CreatedByContact)
                     .Include(t => t.LastUpdatedByContact)
                        .Select(c => new
                        {
                            c.Id,
                            c.Employee.EmployeeId,
                            Name = c.FirstName + " " + c.LastName,
                            Position = c.Postion.Name,
                            BusinessProfile = c.ContactBusinessProfiles.First().BusinessProfile.CompanyName,
                            c.BusinessPhone,
                            c.HomePhone,
                            c.Mobile,
                            c.Email,
                            CreatedOn = c.CreatedDateTime,
                            Active = c.IsActive,
                            CreatedBy = c.CreatedByContact.FirstName + " " + c.CreatedByContact.LastName,
                            UpdatedBy = c.LastUpdatedByContact.FirstName + " " + c.LastUpdatedByContact.LastName
                        });


            return await Task.Run(() => DataSourceLoader.Load(query, options));
        }

        /// <summary>
        /// Get entity detail tab list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TabModel> GetEmployeeDetailsTabs(int id)
        {
            List<TabModel> tabModels = new List<TabModel>
            {
                new TabModel(1, "General Information", "generalInformation")
            };

            if (id == 0)
            {
                return tabModels;
            }

            //tabModels.Add(new TabModel(2, "Address", "addresses"));

            //TODO
            //tabModels.Add(new TabModel(3, "Documents", "documents"));
            //tabModels.Add(new TabModel(4, "Communications", "communications"));
            //tabModels.Add(new TabModel(5, "Compliances", "compliances"));

            return tabModels;
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int id)
        {
            if (id == 0)
            {
                EmployeeModel emptyModel = new EmployeeModel();
                emptyModel.Contact.BusinessProfileIds.Add(LoggedInUser.DefaultBusinessProfileId);
                return emptyModel;
            }

            int loggedInUserBPId = this.LoggedInUser.DefaultBusinessProfileId;
            bool isDefaultBusinessProfileUser = this.LoggedInUser.IsDefaultBusinessProfile;


            Employee employee = await employeeRepository.Where(x => x.Id == id
                    && (x.Contact.ContactBusinessProfiles.Any(cbp => cbp.BusinessProfileId == loggedInUserBPId) || isDefaultBusinessProfileUser))
                .Include(x => x.Contact).ThenInclude(x => x.Photo)
                .Include(x => x.Contact).ThenInclude(t => t.ContactSpecialisations)
                .Include(x => x.Contact).ThenInclude(t => t.EntityContacts)
                .Include(x => x.Contact).ThenInclude(t => t.ContactBusinessProfiles)
                .Include(x => x.EmployeeCostCentres)
                .Include(x => x.EmployeeSites)
                .FirstOrDefaultAsync();


            if (employee == null)
            {
                throw new SoftcodeNotFoundException("Employee not found");
            }

            if (employee.Contact.Photo == null) employee.Contact.Photo = new Photo();
            var model = Mapper.Map<EmployeeModel>(employee);
            if (model != null)
            {
                model.IsActive = model.Contact.IsActive;
                if (!string.IsNullOrEmpty(employee.Contact.DateOfBirth))
                {
                    string decryptedText = Encryption.Decrypt(employee.Contact.DateOfBirth, true);
                    DateTime dt = DateTime.Parse(decryptedText);
                    model.Contact.DateOfBirthDateFormat = dt;
                }

                if (employee.EmployeeCostCentres.Count > 0)
                {
                    model.EmployeeCostCentreIds = employee.EmployeeCostCentres.Select(x => x.CostCentreId).ToList();
                }

                if (employee.EmployeeSites.Count > 0)
                {
                    model.EmployeeSiteIds = employee.EmployeeSites.Select(x => x.SiteId).ToList();
                }


                //skill list
                if (employee.Contact.ContactSpecialisations.Count > 0)
                    model.Contact.ContactSpecialisationIds = employee.Contact.ContactSpecialisations.Select(t => t.SpecialisationId).ToList();

                //business profile list
                if (employee.Contact.ContactBusinessProfiles.Count > 0)
                    model.Contact.BusinessProfileIds = employee.Contact.ContactBusinessProfiles.Select(t => t.BusinessProfileId).ToList();

                //company id list
                if (employee.Contact.EntityContacts.Count > 0)
                    model.Contact.CompanyIds = employee.Contact.EntityContacts.Where(t => t.EntityTypeId == ApplicationEntityType.Company).Select(t => t.EntityId).ToList();

                var entityContact = employee.Contact.EntityContacts.FirstOrDefault(t => t.ContactId == employee.Contact.Id && t.EntityTypeId == ApplicationEntityType.Company);
                if (entityContact != null)
                {
                    model.Contact.CompanyId = entityContact.EntityId;
                }

                //entityContact = employee.Contact.EntityContacts.FirstOrDefault(t => t.ContactId == employee.Contact.Id && t.EntityTypeId == ApplicationEntityType.BusinessProfile);
                //if (entityContact != null)
                //{
                //    model.Contact.BusinessProfileId = entityContact.EntityId;
                //}
            }

            return model;
        }

        public async Task<int> SaveEmployeeAsync(int id, EmployeeModel model)
        {
            if (model == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid Data");
            }
            else
            {
                if (employeeRepository.Where(x => x.EmployeeId == model.EmployeeId && x.Id != id
                                                && x.Contact.ContactBusinessProfiles.Any(bp => bp.BusinessProfileId == model.Contact.BusinessProfileIds[0]))
                    .Include(x => x.Contact).ThenInclude(t => t.ContactBusinessProfiles)
                    .Any())
                {
                    throw new SoftcodeInvalidDataException("Employee Id cannot be duplicated.");
                }

                var result = this.ValidateUserEmail(model.Contact.Id, model.Contact.Email);
                if (!result)
                    throw new SoftcodeInvalidDataException("Email cannot be duplicated.");
            }

            Employee employee = new Employee();


            if (!model.Contact.Photo.IsUpdated && !model.Contact.Photo.IsDeleted)
            {
                model.Contact.Photo = null;
            }

            if (model.Contact.Id > 0)
            {
                if (model.Contact.Photo != null)
                {


                    employee = await employeeRepository.Where(x => x.Id == id)
                    .Include(x => x.Contact).ThenInclude(x => x.Photo)
                    .Include(x => x.Contact).ThenInclude(t => t.ContactSpecialisations)
                    //.Include(x => x.Contact).ThenInclude(t => t.EntityContacts)
                    .Include(x => x.Contact).ThenInclude(t => t.ContactBusinessProfiles)
                    .Include(x => x.EmployeeCostCentres)
                    .Include(x => x.EmployeeSites)
                    .FirstOrDefaultAsync();

                }
                else
                {
                    employee = await employeeRepository.Where(x => x.Id == id)
                     .Include(x => x.Contact).ThenInclude(t => t.ContactSpecialisations)
                     //.Include(x => x.Contact).ThenInclude(t => t.EntityContacts)
                     .Include(x => x.Contact).ThenInclude(t => t.ContactBusinessProfiles)
                     .Include(x => x.EmployeeCostCentres)
                     .Include(x => x.EmployeeSites)
                     .FirstOrDefaultAsync();

                }

                if (employee == null)
                {
                    throw new SoftcodeNotFoundException("Employee not found");
                }
            }



            if (employee != null)
            {
                model.IsActive = model.Contact.IsActive;

                this.Mapper.Map(model, employee);
                //var a  = this.Mapper.Map<List<ApplicationService.Models.ContactAddressModel>, ICollection<ContactAddress>>(model.Contact.ContactAddresses);
                employee.Contact.ContactType = ApplicationContactType.Employee;

                if (model.Contact.DateOfBirthDateFormat != null)
                {
                    string encryptedText =Encryption.Encrypt(model.Contact.DateOfBirthDateFormat.ToString(Utilities.DateFormat));
                    employee.Contact.DateOfBirth = encryptedText;
                }

                if (model.Contact.PhotoId > 0 && model.Contact.Photo != null)
                {
                    if (model.Contact.Photo.IsDeleted)
                    {
                        employee.Contact.PhotoId = null;
                        employee.Contact.Photo = null;
                    }
                    else
                    {
                        employee.Contact.Photo.OrginalFileName = model.Contact.Photo.UploadedFileName;
                        employee.Contact.Photo.PhotoThumb = this.photoService.GetImageFile(model.Contact.Photo.UploadedFileName);
                    }
                }
                else if (!string.IsNullOrEmpty(model.Contact.Photo?.UploadedFileName))
                {
                    Photo photo = new Photo();
                    photo.PhotoThumb = this.photoService.GetImageFile(model.Contact.Photo.UploadedFileName);
                    photo.FileName = model.Contact.DisplayName;
                    photo.OrginalFileName = model.Contact.Photo.UploadedFileName;
                    photo.IsDefault = true;
                    photo.IsVisibleInPublicPortal = true;
                    photo.CreatedDateTime = DateTime.UtcNow;
                    photo.CreatedByContactId = LoggedInUser.ContectId;
                    employee.Contact.Photo = photo;
                }

                //if it is edit mode
                if (model.Id > 0)
                {

                    this.contactBusinessProfileRepository.Remove(contactBusinessProfileRepository.Where(x => x.ContactId == model.Contact.Id
                                && x.EntityTypeId == ApplicationEntityType.BusinessProfile && !model.Contact.BusinessProfileIds.Contains(x.BusinessProfileId)).ToList());
                    //delete previous record

                    this.contactSpecialisationRepository.Remove(contactSpecialisationRepository.Where(x => x.ContactId == model.Contact.Id
                                && !model.Contact.ContactSpecialisationIds.Contains(x.SpecialisationId)).ToList());

                    //delete unselected emplyee site 
                    this.employeeSiteRepository.Remove(employeeSiteRepository.Where(x => x.EmployeeId == model.Id && !model.EmployeeSiteIds.Contains(x.SiteId)).ToList());

                    //delete unselected employee cost centre 
                    this.employeeCostCentreRepository.Remove(employeeCostCentreRepository.Where(x => x.EmployeeId == model.Id && !model.EmployeeCostCentreIds.Contains(x.CostCentreId)).ToList());
                }

                //insert record for specialisation
                foreach (int entityid in model.Contact.ContactSpecialisationIds)
                {
                    if (!employee.Contact.ContactSpecialisations.Where(s => s.SpecialisationId == entityid).Any())
                    {
                        employee.Contact.ContactSpecialisations.Add(new ContactSpecialisation { SpecialisationId = entityid });
                    }
                }

                //insert record for contact business profile 
                foreach (int entityid in model.Contact.BusinessProfileIds)
                {
                    if (!employee.Contact.ContactBusinessProfiles.Where(s => s.BusinessProfileId == entityid).Any())
                    {
                        employee.Contact.ContactBusinessProfiles.Add(new ContactBusinessProfile { BusinessProfileId = entityid, EntityTypeId = ApplicationEntityType.BusinessProfile });
                    }
                }


                //insert new site information
                foreach (int siteId in model.EmployeeSiteIds)
                {
                    if (!employee.EmployeeSites.Where(s => s.SiteId == siteId).Any())
                    {
                        employee.EmployeeSites.Add(new EmployeeSite { SiteId = siteId });
                    }
                }


                //insert new cost centre information
                foreach (int ccid in model.EmployeeCostCentreIds)
                {
                    if (!employee.EmployeeCostCentres.Where(s => s.CostCentreId == ccid).Any())
                    {
                        employee.EmployeeCostCentres.Add(new EmployeeCostCentre { CostCentreId = ccid });
                    }
                }

                if (model.Id == 0)
                {
                    await employeeRepository.CreateAsync(employee);
                }
                else
                {
                    await employeeRepository.UpdateAsync(employee);
                    //Delete Photo
                    if (model.Contact.Photo != null && model.Contact.Photo.IsDeleted)
                    {
                        photoRepository.Delete(p => p.Id == model.Contact.Photo.Id);
                    }
                }

            }
            return employee.Id;
        }

        /// <summary>
        /// check whether this email is used to another user or not
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateUserEmail(int id, string email)
        {
            bool isValidEmail = true;

            //if it is not empty
            if (id > 0)
            {
                Contact obj = this.contactRepository.Where(t => t.Id != id && t.Email == email).FirstOrDefault();
                if (obj != null)
                {
                    isValidEmail = false;
                }
            }
            else
            {
                Contact obj = this.contactRepository.Where(t => t.Email == email).FirstOrDefault();
                if (obj != null)
                {
                    isValidEmail = false;
                }
            }

            return isValidEmail;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            if (id < 1)
            {
                throw new SoftcodeArgumentMissingException("Contact not found");
            }

            if (!await contactRepository.ExistsAsync(x => x.Id == id))
            {
                throw new SoftcodeArgumentMissingException("Contact not found");
            }
            var photoid = contactRepository.FindOne(x => x.Id == id).PhotoId;

            //delete company entity
            this.entityContactRepository.Remove(this.entityContactRepository.Where(t => t.ContactId == id && t.EntityTypeId == ApplicationEntityType.Company).ToList());
            //delete business profile entity
            this.contactBusinessProfileRepository.Remove(this.contactBusinessProfileRepository.Where(t => t.ContactId == id && t.EntityTypeId == ApplicationEntityType.BusinessProfile).ToList());
            //delete contact Specialisation
            this.contactSpecialisationRepository.Remove(this.contactSpecialisationRepository.Where(t => t.ContactId == id).ToList());
            //delete employee CostCentre
            this.employeeCostCentreRepository.Remove(this.employeeCostCentreRepository.Where(t => t.EmployeeId == id).ToList());
            //delete employee Site
            this.employeeSiteRepository.Remove(this.employeeSiteRepository.Where(t => t.EmployeeId == id).ToList());
            //delete employee data
            this.employeeRepository.Remove(this.employeeRepository.Where(t => t.Id == id).ToList());
            //delete contact data
            await contactRepository.DeleteAsync(t => t.Id == id);

            //Delete logo

            if (photoid > 0)
            {
                photoRepository.Delete(p => p.Id == photoid);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEmployeesAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SupervisorModel>> GetSupervisorListByBPAsync(int businessProfileId, int empId)
        {
            var query = from c in this.contactRepository.Where(c => c.IsActive && c.Id != empId && c.ContactType == Configuration.ApplicationContactType.Employee
                        && (c.ContactBusinessProfiles.Any(cbp => cbp.BusinessProfileId == businessProfileId)))
                     //.Include(t => t.Employee)
                     .Include(t => t.ContactBusinessProfiles).Include(t => t.EntityContacts)
                        select new SupervisorModel
                        {

                            Id = c.Id,
                            //EmployeeId = c.Employee.EmployeeId,
                            Name = c.FirstName + " " + c.LastName,
                            Position = c.Postion.Name
                        };


            return await Task.Run(() => query.ToList());
        }

       
    }
}
