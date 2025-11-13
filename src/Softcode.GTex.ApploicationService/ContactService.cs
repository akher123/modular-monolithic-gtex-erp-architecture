using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public class ContactService : BaseService<ContactService>, IContactService
    {
        private readonly IRepository<Contact> contactRepository;
        private readonly IRepository<ContactSpecialisation> contactSpecialisationRepository;
        private readonly IRepository<EntityContact> entityContactRepository;
        private readonly IRepository<ContactBusinessProfile> businessProfileContactRepository;
        private readonly IRepository<Photo> photoRepository;
        private readonly IPhotoService photoService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContactService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<EntityContact> entityContactRepository
            , IRepository<ContactSpecialisation> contactSpecialisationRepository
            , IRepository<Contact> contactRepository
            , IRepository<Photo> photoRepository
            , IPhotoService photoService
            , IRepository<ContactBusinessProfile> businessProfileContactRepository
            , UserManager<ApplicationUser> userManager) : base(applicationServiceBuilder)
        {
            this.businessProfileContactRepository = businessProfileContactRepository;
            this.entityContactRepository = entityContactRepository;
            this.contactSpecialisationRepository = contactSpecialisationRepository;
            this.contactRepository = contactRepository;
            this.photoService = photoService;
            this.photoRepository = photoRepository;
            this.userManager = userManager;
        }

        #region Get Methods


        /// <summary>
        /// Get contacts by company
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<LoadResult> GetContactListAsync(DataSourceLoadOptionsBase options)
        {

            IQueryable<Contact> contactQuery = this.contactRepository.Where(c => c.ContactType == Configuration.ApplicationContactType.Contact).Include(t => t.Gender)
                        .Include(t => t.PreferredContactMethod)
                        .Include(t => t.ContactBusinessProfiles).Include(t => t.EntityContacts);

            if (!LoggedInUser.IsSuperAdmin)
            {
                int businessProfileId = LoggedInUser.DefaultBusinessProfileId;
                contactQuery = contactQuery.Where(x => x.ContactBusinessProfiles.Any(c => c.BusinessProfileId == businessProfileId));
            }


            var query = contactQuery.Select(c => new
            {
                c.Id,
                Name = c.FirstName + " " + c.LastName,
                //BusinessProfile = string.Join(", ", c.ContactBusinessProfiles.Select(p => p.BusinessProfile.CompanyName.ToString())),
                BusinessProfile = c.ContactBusinessProfiles.Select(p => " " + p.BusinessProfile.CompanyName.ToString()),
                //CompanyName = string.Join(", ", c.EntityContacts.Select(p => p.Company.CompanyName.ToString())),
                CompanyName = c.EntityContacts.Select(p => " " + p.Company.CompanyName.ToString()),
                Gender = c.Gender != null ? c.Gender.Name : "",               
                c.BusinessPhone,
                c.HomePhone,
                c.Mobile,
                c.Email,
                c.Fax,
                Active = c.IsActive,
                PreferredContactMethod = c.PreferredContactMethod != null ? c.PreferredContactMethod.Name : "",
                CreatedOn = c.CreatedDateTime

            });


            
            options.Select = new[] { "Id", "Name", "BusinessProfile", "CompanyName", "Gender", "BusinessPhone", "Mobile", "Email", "Fax", "Active", "PreferredContactMethod", "CreatedOn" };

            return await Task.Run(() => DataSourceLoader.Load(query, options));
        }

      
        /// <summary>
        /// Get Entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ContactModel> GetContactModelByIdAsync(int id)
        {
            if (id == 0)
            {
                ContactModel emptyModel = new ContactModel();
                emptyModel.BusinessProfileIds.Add(LoggedInUser.DefaultBusinessProfileId);
                return emptyModel;
            }

            Contact contact = await contactRepository.Where(x => x.Id == id)
                                    .Include(x => x.Photo)
                                    .Include(t => t.ContactSpecialisations)
                                    .Include(t => t.EntityContacts)
                                    .Include(t => t.ContactBusinessProfiles).FirstOrDefaultAsync();

            if (contact == null)
            {
                throw new SoftcodeNotFoundException("Contact not found");
            }

            if (contact.Photo == null) contact.Photo = new Photo();
            var obj = Mapper.Map<ContactModel>(contact);
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(contact.DateOfBirth))
                {
                    string decryptedText = Encryption.Decrypt(contact.DateOfBirth, true);
                    DateTime dt = DateTime.Parse(decryptedText);
                    obj.DateOfBirthDateFormat = dt;
                }

                //skill list
                if (contact.ContactSpecialisations.Count > 0)
                    obj.ContactSpecialisationIds = contact.ContactSpecialisations.Select(t => t.SpecialisationId).ToList();

                //business profile list
                if (contact.ContactBusinessProfiles.Count > 0)
                    obj.BusinessProfileIds = contact.ContactBusinessProfiles.Select(t => t.BusinessProfileId).ToList();

                //company id list
                if (contact.EntityContacts.Count > 0)
                    obj.CompanyIds = contact.EntityContacts.Where(t => t.EntityTypeId == ApplicationEntityType.Company).Select(t => t.EntityId).ToList();

              
            }

            return obj;
        }
        /// <summary>
        /// get entity for user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ContactModel> GetContactForUserByContactIdAsync(int id)
        {
            if (id == 0)
            {
                return new ContactModel();
            }

            Contact contact = await contactRepository.Where(x => x.Id == id).Include(x => x.Photo).FirstOrDefaultAsync();
            if (contact == null)
            {
                throw new SoftcodeNotFoundException("Contact not found");
            }

            if (contact.Photo == null) contact.Photo = new Photo();
            ContactModel contactModel = Mapper.Map<ContactModel>(contact);
            if (contactModel != null)
            {
                //business profile list
                if (contact.ContactBusinessProfiles.Count > 0)
                    contactModel.BusinessProfileIds = contact.ContactBusinessProfiles.Select(t => t.BusinessProfileId).ToList();
            }

            return contactModel;

        }
        /// <summary>
        /// Get entity detail tab list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TabModel> GetContactDetailsTabs(int id)
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


        public async Task<List<SelectModel>> GetContactSelectItemsByBusinessProfileIdAsync(int businessProfileId)
        {
            if (businessProfileId == 0)
            {
                businessProfileId = this.LoggedInUser.DefaultBusinessProfileId;
            }

            return await Task.Run(() => businessProfileContactRepository
            .Where(x => x.BusinessProfileId == businessProfileId && x.Contact.IsActive == true && x.Contact.ContactType == ApplicationContactType.Contact).Include(c => c.Contact)
            .Select(x => new SelectModel
            {
                Id = x.Contact.Id,
                Name = x.Contact.FirstName + " " + x.Contact.LastName
            }).ToList());
        }

        public async Task<List<SelectModel>> GetContactSelectItemsByBPIdAndContactTypeAsync(int businessProfileId, int contactTypeId)
        {
            if (businessProfileId == 0)
            {
                businessProfileId = this.LoggedInUser.DefaultBusinessProfileId;
            }

            return await Task.Run(() => businessProfileContactRepository
            .Where(x => x.BusinessProfileId == businessProfileId && x.Contact.IsActive == true && x.Contact.ContactType == contactTypeId).Include(c => c.Contact)
            .Select(x => new SelectModel
            {
                Id = x.Contact.Id,
                Name = x.Contact.FirstName + " " + x.Contact.LastName
            }).ToList());
        }


        #endregion

        #region Insert/Update/Delete Methods

        /// <summary>
        /// Insert/update entity
        /// </summary>
        /// <param name="contactModel"></param>
        /// <returns></returns>
        public async Task<int> SaveContactDetailsAsync(ContactModel contactModel)
        {
            if (contactModel == null)
            {
                throw new SoftcodeArgumentMissingException("Contact not found");
            }
            else
            {
                var result = this.ValidateUserEmail(contactModel.Id, contactModel.Email);
                if (!result)
                    throw new SoftcodeInvalidDataException("Email cannot be duplicated.");

            }

            Contact contact = new Contact();

            if (!contactModel.Photo.IsUpdated && !contactModel.Photo.IsDeleted)
            {
                contactModel.Photo = null;
            }

            if (contactModel.Id > 0)
            {
                if (contactModel.Photo != null)
                {
                    contact = contactRepository.Where(b => b.Id == contactModel.Id).Include(p => p.Photo).Include(t => t.EntityContacts).Include(t => t.ContactBusinessProfiles).Include(t => t.ContactSpecialisations).FirstOrDefault();
                }
                else
                {
                    contact = contactRepository.Where(b => b.Id == contactModel.Id).Include(t => t.EntityContacts).Include(t => t.ContactSpecialisations).Include(t => t.ContactBusinessProfiles).FirstOrDefault();
                }

                if (contact == null)
                {
                    throw new SoftcodeNotFoundException("Contact not found for edit");
                }
            }


            if (contact != null)
            {
                Mapper.Map(contactModel, contact);
                contact.ContactType = ApplicationContactType.Contact;

                if (contactModel.DateOfBirthDateFormat != null)
                {
                    string encryptedText = Encryption.Encrypt(contactModel.DateOfBirthDateFormat.ToString(Utilities.DateFormat));
                    contact.DateOfBirth = encryptedText;
                }

                if (contactModel.PhotoId > 0 && contactModel.Photo != null)
                {
                    if (contactModel.Photo.IsDeleted)
                    {
                        contact.PhotoId = null;
                        contact.Photo = null;
                    }
                    else
                    {
                        contact.Photo.OrginalFileName = contactModel.Photo.UploadedFileName;
                        contact.Photo.PhotoThumb = this.photoService.GetImageFile(contactModel.Photo.UploadedFileName);
                    }
                }
                else if (!string.IsNullOrEmpty(contactModel.Photo?.UploadedFileName))
                {
                    Photo photo = new Photo();
                    photo.PhotoThumb = this.photoService.GetImageFile(contactModel.Photo.UploadedFileName);
                    photo.FileName = contactModel.DisplayName;
                    photo.OrginalFileName = contactModel.Photo.UploadedFileName;
                    photo.IsDefault = true;
                    photo.IsVisibleInPublicPortal = true;
                    photo.CreatedDateTime = DateTime.UtcNow;
                    photo.CreatedByContactId = LoggedInUser.ContectId;
                    contact.Photo = photo;
                }

                //if it is edit mode
                if (contactModel.Id > 0)
                {
                    //delete company entity
                    this.entityContactRepository.Delete(t => t.ContactId == contactModel.Id && t.EntityTypeId == ApplicationEntityType.Company);
                    //delete business profile entity
                    this.businessProfileContactRepository.Delete(t => t.ContactId == contactModel.Id && t.EntityTypeId == ApplicationEntityType.BusinessProfile);
                    //delete previous record
                    this.contactSpecialisationRepository.Delete(t => t.ContactId == contactModel.Id);
                }

                //insert record for specialisation
                foreach (int entityid in contactModel.ContactSpecialisationIds)
                {
                    ContactSpecialisation contactSpecialisation = new ContactSpecialisation();
                    contactSpecialisation.SpecialisationId = entityid;
                    contact.ContactSpecialisations.Add(contactSpecialisation);
                }

                //insert record for contact business profile 
                foreach (int entityid in contactModel.BusinessProfileIds)
                {
                    ContactBusinessProfile contactBusinessProfile = new ContactBusinessProfile();
                    contactBusinessProfile.EntityTypeId = ApplicationEntityType.BusinessProfile;
                    contactBusinessProfile.BusinessProfileId = entityid;
                    contact.ContactBusinessProfiles.Add(contactBusinessProfile);
                }

                //insert company information
                foreach (int entityid in contactModel.CompanyIds)
                {
                    EntityContact entityContact = new EntityContact();
                    entityContact.EntityTypeId = ApplicationEntityType.Company;
                    entityContact.EntityId = entityid;
                    contact.EntityContacts.Add(entityContact);
                }

                if (contactModel.Id == 0)
                {
                    await contactRepository.CreateAsync(contact);
                }
                else
                {
                    await contactRepository.UpdateAsync(contact);
                    //Delete Photo
                    if (contactModel.Photo != null && contactModel.Photo.IsDeleted)
                    {
                        photoRepository.Delete(p => p.Id == contactModel.Photo.Id);
                    }
                }

            }
            return contact.Id;
        }


        /// <summary>
        /// Delete entities by id list
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteContactAsync(int id)
        {
            if (id < 1)
            {
                throw new SoftcodeArgumentMissingException("Contact not found");
            }

            if (!await contactRepository.ExistsAsync(x => x.Id == id))
            {
                throw new SoftcodeArgumentMissingException("Contact not found");
            }

            //delete company entity
            this.entityContactRepository.Delete(t => t.ContactId == id && t.EntityTypeId == ApplicationEntityType.Company);
            //delete business profile entity
            this.businessProfileContactRepository.Delete(t => t.ContactId == id && t.EntityTypeId == ApplicationEntityType.BusinessProfile);
            //delete previous record
            this.contactSpecialisationRepository.Delete(t => t.ContactId == id);

            return await contactRepository.DeleteAsync(t => t.Id == id) > 0;
        }

        /// <summary>
        /// Delete entities by id list
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteContactAsync(List<int> ids)
        {
            if (ids == null)
            {
                throw new SoftcodeArgumentMissingException("Contact not found");
            }
            foreach (int id in ids)
            {
                if (!await contactRepository.ExistsAsync(x => ids.Contains(x.Id)))
                {
                    throw new SoftcodeArgumentMissingException("Contact not found");
                }


                //delete company entity
                this.entityContactRepository.Delete(t => t.ContactId == id && t.EntityTypeId == ApplicationEntityType.Company);
                //delete business profile entity
                this.businessProfileContactRepository.Delete(t => t.ContactId == id && t.EntityTypeId == ApplicationEntityType.BusinessProfile);
                //delete previous record
                this.contactSpecialisationRepository.Delete(t => t.ContactId == id);

                await contactRepository.DeleteAsync(t => t.Id == id);
            }
            return true;
        }

        #endregion

        #region Validations Methods

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

        #endregion

        #region Get Methods For Controls [Combobox, Listbox, etc]

        /// <summary>
        /// get preferred phone type
        /// </summary>
        /// <returns></returns>
        public List<SelectModel> GetPreferredPhoneTypeSelectedItem()
        {
            return Utilities.GetEnumValueList(typeof(Configuration.Enums.PreferredPhoneType));
        }
        /// <summary>
        /// Contacts for selection controls
        /// </summary>
        /// <returns></returns>
        public List<SelectModel> ContactSelectItems()
        {
            return contactRepository.Where(x => x.IsActive).Select(x => new SelectModel { Id = x.Id, Name = x.DisplayName }).ToList();
        }
        /// <summary>
        /// get gender type
        /// </summary>
        /// <returns></returns>
        public List<SelectModel> GetGenderTypeSelectedItem()
        {
            return Utilities.GetEnumValueList(typeof(Configuration.Enums.GenderType));
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="businessprofileIds"></param>
        ///// <returns></returns>
        //public List<SelectModel> GetContactSelectItemsAsync(List<int> businessprofileIds)
        //{
        //    var query = from c in this.contactRepository.GetAll()
        //                join cb in this.businessProfileContactRepository.GetAll() on c.Id equals cb.ContactId
        //                where c.IsActive == true && c.ContactType == ApplicationContactType.Contact
        //                select new SelectModel
        //                {
        //                    Id = c.Id,
        //                    Name = c.FirstName + " " + c.LastName + " (email: " + c.Email + ")"
        //                };


        //    return query.Distinct().ToList<SelectModel>();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessprofileIds"></param>
        /// <returns></returns>
        public async Task<List<SelectModel>> GetContactSelectItemsForUserAsync(List<int> businessprofileIds)
        {
            var query = await Task.Run(() => (from c in this.contactRepository.GetAll()
                                              join cb in this.businessProfileContactRepository.GetAll() on c.Id equals cb.ContactId
                                              join u in this.userManager.Users on c.Id equals u.ContactId into cu
                                              from u in cu.DefaultIfEmpty()
                                              where c.IsActive == true && u.Id == null && c.ContactType == ApplicationContactType.Contact
                                              && businessprofileIds.Contains(cb.BusinessProfileId)
                                              select new
                                              {
                                                  Id = c.Id,
                                                  Name = c.FirstName + " " + c.LastName + " (email: " + c.Email + ")",
                                                  BusinessProfileIds = c.ContactBusinessProfiles.Select(x => x.BusinessProfileId).ToList()
                                              }).Distinct().ToList());




            return query.Where(x => businessprofileIds.All(x.BusinessProfileIds.Contains)).Select(x => new SelectModel { Id = x.Id, Name = x.Name }).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessprofileIds"></param>
        /// <returns></returns>
        public async Task<List<SelectModel>> GetEmployeeSelectItemsForUserAsync(List<int> businessprofileIds)
        {
            var query = await Task.Run(() => (from c in this.contactRepository.GetAll()
                                              join cb in this.businessProfileContactRepository.GetAll() on c.Id equals cb.ContactId
                                              join u in this.userManager.Users on c.Id equals u.ContactId into cu
                                              from u in cu.DefaultIfEmpty()
                                              where c.IsActive == true && u.Id == null && c.ContactType == ApplicationContactType.Employee
                                              && businessprofileIds.Contains(cb.BusinessProfileId)
                                              select new
                                              {
                                                  Id = c.Id,
                                                  Name = c.FirstName + " " + c.LastName + " (email: " + c.Email + ")",
                                                  BusinessProfileIds = c.ContactBusinessProfiles.Select(x => x.BusinessProfileId).ToList()
                                              }).Distinct().ToList());

            return query.Where(x => businessprofileIds.All(x.BusinessProfileIds.Contains)).Select(x => new SelectModel { Id = x.Id, Name = x.Name }).ToList();
        }


        #endregion
    }
}
