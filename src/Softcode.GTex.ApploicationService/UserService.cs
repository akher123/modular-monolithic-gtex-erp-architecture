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
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public class UserService : BaseService<UserService>, IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IRepository<Photo> photoRepository;
        private readonly IRepository<ContactBusinessProfile> contactBusinessProfileRepository;        
        private readonly IRepository<UserBusinessProfile> userBusinessProfileRepository;
        private readonly IRepository<Contact> contactRepository;
        private readonly IRepository<RecordInfo> recordInfoRepository;
        private readonly IPhotoService photoService;
        private readonly IRoleService roleService;
        private readonly ISecurityProfileService securityProfileService;

        /// <summary>
        /// User Service
        /// </summary>
        /// <param name="applicationServiceBuilder"></param>
        /// <param name="photoService"></param>
        /// <param name="businessProfileContactRepository"></param>
        /// <param name="userBusinessProfileRepository"></param>
        /// <param name="photoRepository"></param>
        /// <param name="contactRepository"></param>
        /// <param name="recordInfoRepository"></param>
        /// <param name="roleService"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="securityProfileService"></param>

        public UserService(IApplicationServiceBuilder applicationServiceBuilder
            , IPhotoService photoService
            , IRepository<ContactBusinessProfile> businessProfileContactRepository
            , IRepository<UserBusinessProfile> userBusinessProfileRepository
            , IRepository<Photo> photoRepository
            , IRepository<Contact> contactRepository
            , IRepository<RecordInfo> recordInfoRepository
            , IRoleService roleService
            , UserManager<ApplicationUser> userManager
            , RoleManager<ApplicationRole> roleManager
            , ISecurityProfileService securityProfileService
            ) : base(applicationServiceBuilder)
        {
            this.userManager = userManager;
            this.photoRepository = photoRepository;
            this.photoService = photoService;
            this.roleService = roleService;
            this.roleManager = roleManager;
            this.contactBusinessProfileRepository = businessProfileContactRepository;
            this.contactRepository = contactRepository;
            this.recordInfoRepository = recordInfoRepository;
            this.securityProfileService = securityProfileService;
            this.userBusinessProfileRepository = userBusinessProfileRepository;

        }
        ///// <summary>
        ///// Get active user
        ///// </summary>
        ///// <param name="businessProfileId"></param>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public async Task<LoadResult> GetActiveUsersAsync(Guid? businessProfileId, DataSourceLoadOptionsBase options)
        //{
        //    IQueryable<ApplicationUser> usersQuery = userManager.Users.Where(x => x.IsActive);
        //    return await Task.Run(() => DataSourceLoader.Load(usersQuery, options));
        //}


        /// <summary>
        /// Get users list
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<LoadResult> GetUserListAsync(DataSourceLoadOptionsBase options)
        {
            IQueryable<ApplicationUser> usersQuery = userManager.Users.Where(u => u.ContactTypeId != Configuration.ApplicationContactType.ServiceUser)
                .Include(u => u.Contact).Include(c => c.UserBusinessProfiles);


            if (!LoggedInUser.IsSuperAdmin)
            {
                int businessProfileId = LoggedInUser.DefaultBusinessProfileId;
                usersQuery = usersQuery.Where(x => x.UserBusinessProfiles.Any(c => c.BusinessProfileId == businessProfileId));
            }

            options.Select = new[] { "Id", "UserName", "Contact.DisplayName", "Contact.BusinessPhone", "Contact.Mobile", "Contact.Email", "IsActive" };
            return await Task.Run(() => DataSourceLoader.Load(usersQuery, options));
        }
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationUserModel> GetUserModelByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new ApplicationUserModel();
            }

            ApplicationUser user = await userManager.Users.Where(t => t.Id == id)
                .Include(t => t.UserBusinessProfiles)
                .Include(t => t.Contact).ThenInclude(t => t.Photo)
                .Include(t => t.Contact).ThenInclude(t => t.ContactBusinessProfiles).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new SoftcodeNotFoundException("Application User not found");
            }

            ApplicationUserModel userModel = Mapper.Map<ApplicationUserModel>(user);
            if (userModel != null)
            {
                //Set User Roles
                IList<string> roleNameList = await userManager.GetRolesAsync(user);
                List<string> roleList = roleManager.Roles.Where(t => roleNameList.Contains(t.NormalizedName)).Select(t => t.Id).ToList();
                //Check Super Admin
                if (roleNameList.Contains(ApplicationConstants.SuperAdmin))
                {
                    //Check Super Admin
                    userModel.IsSuperAdmin = true;// roleList.Any(x => x == this.ApplicationCacheService.GetSuperAdminRole().Id);
                    roleList = roleManager.Roles.Where(t => t.IsActive).Select(t => t.Id).ToList();
                }
                else
                {
                    roleList = roleManager.Roles.Where(t => t.IsActive && roleNameList.Contains(t.NormalizedName)).Select(t => t.Id).ToList();
                }

                userModel.UserRoles = roleList;


                if (userModel.Contact.Photo == null)
                {
                    userModel.Contact.Photo = new PhotoModel();
                }

                //Contact business profile list
                if (user.Contact.ContactBusinessProfiles.Count > 0)
                {
                    userModel.Contact.BusinessProfileIds = user.Contact.ContactBusinessProfiles.Select(t => t.BusinessProfileId).ToList();
                }

                //User business profile list
                if (user.UserBusinessProfiles.Count > 0)
                    userModel.BusinessProfileIds = user.UserBusinessProfiles.Select(t => t.BusinessProfileId).ToList();
                    userModel.BusinessProfileId = userModel.BusinessProfileIds.First();
                if (user.LockoutEnd != null && user.LockoutEnd.Value.LocalDateTime > DateTimeOffset.Now)
                {
                    userModel.SystemLocked = true;
                }
            }

            return userModel;
        }
        /// <summary>
        /// Insert user detail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> InsertUserDetailAsync(ApplicationUserModel userModel)
        {
            if (userModel == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid user data");
            }
            else
            {
                var result = this.ValidateUserEmail(string.Empty, userModel.Email);
                if (!result)
                    throw new SoftcodeArgumentMissingException("Email cannot be duplicated.");

                result = this.ValidateUserName(string.Empty, userModel.UserName);
                if (!result)
                    throw new SoftcodeArgumentMissingException("Username cannot be duplicated.");
            }

            //password validation
            Dictionary<string, string> passwordErrors = await securityProfileService.CheckPasswordAsync(userModel.SecurityProfileId.Value, userModel.NewPassword, userModel.UserName, userModel.Contact.DisplayName);

            if (passwordErrors.Any())
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var error in passwordErrors)
                {
                    stringBuilder.AppendLine(error.Value);
                }

                throw new SoftcodeInvalidDataException(stringBuilder.ToString());
            }

            ApplicationUser applicationUser = new ApplicationUser();

            if (!userModel.Contact.Photo.IsUpdated && !userModel.Contact.Photo.IsDeleted)
            {
                userModel.Contact.Photo = null;
            }

            if (applicationUser != null)
            {
                userModel.UserName = userModel.ShortUserName + userModel.DomainName;// Concatenate ShortUserName+domainName

                Mapper.Map(userModel, applicationUser);

                //application user
          
                applicationUser.Id = Guid.NewGuid().ToString();
                applicationUser.NormalizedUserName = userModel.UserName;
                applicationUser.NormalizedEmail = userModel.Email;
                applicationUser.EmailConfirmed = true;
                applicationUser.PhoneNumber = userModel.Contact.BusinessPhone;
                applicationUser.TwoFactorEnabled = false;
                applicationUser.CreatedDateTime = DateTime.UtcNow;
                applicationUser.CreatedByContactId = LoggedInUser.ContectId;
                applicationUser.IsActive = true;
                applicationUser.ContactTypeId = ApplicationContactType.User;
                applicationUser.LockoutEnabled = true;

                //TODO: Will remove after implementing EmailConfirmed & PhoneNumberConfirmed
                applicationUser.EmailConfirmed = true;
                applicationUser.PhoneNumberConfirmed = true;

                if (userModel.Contact.Id == 0)
                {
                    applicationUser.Contact.BusinessPhone = userModel.Contact.BusinessPhone;
                    applicationUser.Contact.Mobile = userModel.Contact.Mobile;
                    applicationUser.Contact.Email = applicationUser.Email;
                    applicationUser.Contact.CreatedDateTime = DateTime.UtcNow;
                    applicationUser.Contact.CreatedByContactId = LoggedInUser.ContectId;
                    applicationUser.Contact.ContactType = ApplicationContactType.User;

                    ///update record info manually because we are not using itm repository to save this information
                    applicationUser.Contact.RecordInfo = new RecordInfo();
                    applicationUser.Contact.UniqueEntityId = applicationUser.Contact.RecordInfo.Id;
                    applicationUser.Contact.RecordInfo.EntityTypeId = ApplicationEntityType.Contact;

                    //insert record into contact/user business profile table for user time entity
                    foreach (int bpId in userModel.BusinessProfileIds)
                    {

                        applicationUser.Contact.ContactBusinessProfiles.Add(new ContactBusinessProfile
                        {
                            EntityTypeId = ApplicationEntityType.BusinessProfile,
                            BusinessProfileId = bpId
                        });

                        applicationUser.UserBusinessProfiles.Add(new UserBusinessProfile { BusinessProfileId = bpId });
                    }
                }
                else
                {
                    var contact = this.contactRepository.Where(t => t.Id == userModel.Contact.Id).FirstOrDefault();
                    applicationUser.Contact = contact;
                    applicationUser.ContactTypeId = contact.ContactType;

                    //insert record into user business profile table for new user
                    foreach (int bpId in userModel.BusinessProfileIds)
                    {
                        applicationUser.UserBusinessProfiles.Add(new UserBusinessProfile { BusinessProfileId = bpId });
                    }
                }

                //photo entity 
                if (!string.IsNullOrEmpty(userModel.Contact.Photo?.UploadedFileName))
                {
                    Photo photo = new Photo();
                    photo.PhotoThumb = this.photoService.GetImageFile(userModel.Contact.Photo.UploadedFileName);
                    photo.FileName = userModel.Contact.FirstName + "_" + userModel.Contact.LastName;
                    photo.IsDefault = true;
                    photo.IsVisibleInPublicPortal = true;
                    photo.CreatedDateTime = DateTime.UtcNow;
                    photo.CreatedByContactId = LoggedInUser.ContectId;
                    applicationUser.Contact.Photo = photo;
                }

                await userManager.CreateAsync(applicationUser, userModel.NewPassword);

                //Save Role information
                if (userModel.UserRoles != null)
                {
                    List<string> roleNameList = roleService.GetRoleNameList(userModel.UserRoles);
                    await userManager.AddToRolesAsync(applicationUser, roleNameList);
                }
            }
            return applicationUser.Id;
        }
        /// <summary>
        /// Update user detail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<string> UpdateUserDetailAsync(string id, ApplicationUserModel userModel)
        {
            if (userModel == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid user data");
            }
            else
            {
                var result = this.ValidateUserEmail(id, userModel.Email);
                if (!result)
                    throw new SoftcodeInvalidDataException("Email cannot be duplicated.");

                result = this.ValidateUserName(id, userModel.UserName);
                if (!result)
                    throw new SoftcodeInvalidDataException("Username cannot be duplicated.");


                if (!string.IsNullOrWhiteSpace(userModel.NewPassword))
                {
                    //password validation
                    Dictionary<string, string> passwordErrors = await securityProfileService.CheckPasswordAsync(userModel.SecurityProfileId.Value, userModel.NewPassword, userModel.UserName, userModel.Contact.DisplayName);

                    if (passwordErrors.Any())
                    {
                        StringBuilder stringBuilder = new StringBuilder();

                        foreach (var error in passwordErrors)
                        {
                            stringBuilder.AppendLine(error.Value);
                        }

                        throw new SoftcodeInvalidDataException(stringBuilder.ToString());
                    }
                }
            }

            ApplicationUser applicationUser = new ApplicationUser();

            if (!userModel.Contact.Photo.IsUpdated && !userModel.Contact.Photo.IsDeleted)
            {
                userModel.Contact.Photo = null;
            }

            if (userModel.Contact.Photo != null)
            {
                //Load user with photo
                applicationUser = await userManager.Users.Where(t => t.Id == id)
                    .Include(t => t.UserBusinessProfiles)
                    .Include(t => t.Contact).ThenInclude(t => t.Photo)
                    .Include(t => t.SecurityProfile)
                    .FirstOrDefaultAsync();
            }
            else
            {
                //Load user with out photo
                applicationUser = await userManager.Users.Where(t => t.Id == id)
                    .Include(t => t.UserBusinessProfiles)
                    .Include(t => t.Contact)
                    .Include(t => t.SecurityProfile)
                    .FirstOrDefaultAsync();
            }

            if (applicationUser == null)
            {
                throw new SoftcodeNotFoundException("Application User not found");
            }

            if (applicationUser != null)
            {
                Mapper.Map(userModel, applicationUser);
                applicationUser.Contact.Email = userModel.Email;
                applicationUser.PhoneNumber = userModel.Contact.BusinessPhone;
                applicationUser.NormalizedUserName = userModel.UserName;
                applicationUser.NormalizedEmail = userModel.Email;
                applicationUser.LockoutEnd = null;

                if (userModel.SystemLocked && (applicationUser.LockoutEnd == null || applicationUser.LockoutEnd.Value.LocalDateTime < DateTimeOffset.Now))
                {
                    applicationUser.LockoutEnd = DateTimeOffset.Now.DateTime.AddMinutes(applicationUser.SecurityProfile.LockoutDuration);
                }

                #region Photo Entity

                //update or insert photo entity
                if (userModel.Contact.PhotoId > 0 && userModel.Contact.Photo != null)
                {
                    if (userModel.Contact.Photo.IsDeleted)
                    {
                        applicationUser.Contact.PhotoId = null;
                        applicationUser.Contact.Photo = null;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(userModel.Contact.Photo?.UploadedFileName))
                        {
                            applicationUser.Contact.Photo.OrginalFileName = userModel.Contact.Photo.UploadedFileName;
                            applicationUser.Contact.Photo.PhotoThumb = this.photoService.GetImageFile(userModel.Contact.Photo.UploadedFileName);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(userModel.Contact.Photo?.UploadedFileName))
                {
                    Photo photo = new Photo();
                    photo.PhotoThumb = this.photoService.GetImageFile(userModel.Contact.Photo.UploadedFileName);
                    photo.FileName = userModel.Contact.FirstName + "_" + userModel.Contact.LastName;
                    photo.OrginalFileName = userModel.Contact.Photo.UploadedFileName;
                    photo.IsDefault = true;
                    photo.IsVisibleInPublicPortal = true;
                    photo.CreatedDateTime = DateTime.UtcNow;
                    photo.CreatedByContactId = LoggedInUser.ContectId;
                    applicationUser.Contact.Photo = photo;
                }

                //if (userModel.UserRoles != null)
                //{
                //    List<string> roleNameList = roleService.GetRoleNameList(userModel.UserRoles);
                //    await userManager.AddToRolesAsync(applicationUser, roleNameList);
                //}
                #endregion

                //LoggedIn user cannot modify his BP information
                if (LoggedInUser.UserId != applicationUser.Id)
                {
                    List<int> existingBP = applicationUser.UserBusinessProfiles.Select(x=>x.BusinessProfileId).ToList();

                    if (!LoggedInUser.IsSuperAdmin) {
                        existingBP = applicationUser.UserBusinessProfiles.Select(x => x.BusinessProfileId).Intersect(LoggedInUser.UserBusinessProfileIds).ToList();
                    }

                    //remove exiting business profile                    
                    this.userBusinessProfileRepository.Remove(this.userBusinessProfileRepository.Where(x => x.UserId == userModel.Id
                                && !userModel.BusinessProfileIds.Contains(x.BusinessProfileId)
                                && existingBP.Contains(x.BusinessProfileId)
                                ).ToList());

                    //insert record for Business Profile
                    foreach (int bpId in userModel.BusinessProfileIds)
                    {
                        if (!applicationUser.UserBusinessProfiles.Any(x => x.BusinessProfileId == bpId))
                        {
                            applicationUser.UserBusinessProfiles.Add(new UserBusinessProfile { BusinessProfileId = bpId });
                        }
                    }
                }

                await userManager.UpdateAsync(applicationUser);

                if (!string.IsNullOrWhiteSpace(userModel.NewPassword))
                {
                    await userManager.RemovePasswordAsync(applicationUser);
                    await userManager.AddPasswordAsync(applicationUser, userModel.NewPassword);
                }

                //update roles
                if (LoggedInUser.UserId != applicationUser.Id)
                {
                    if (userModel.UserRoles != null)
                    {
                        List<string> existingRoles = roleService.GetRoleNameList(LoggedInUser.DefaultBusinessProfileRoleHierarchyIds.ToList())
                            .Intersect(userManager.GetRolesAsync(applicationUser).Result).ToList();
                        //List<string> existingRoles = userManager.GetRolesAsync(applicationUser).Result.ToList();
                        //Remove all existing roles
                        if (existingRoles != null && existingRoles.Any())
                        {
                            await userManager.RemoveFromRolesAsync(applicationUser, existingRoles);
                        }
                        //Insert rolea  
                        List<string> roleNameList = roleService.GetRoleNameList(userModel.UserRoles);
                        if (existingRoles != null && roleNameList.Any())
                        {
                            await userManager.AddToRolesAsync(applicationUser, roleNameList);
                        }
                    }
                }

                //Delete photo
                if (userModel.Contact.Photo != null && userModel.Contact.Photo.IsDeleted)
                {
                    photoRepository.Delete(p => p.Id == userModel.Contact.Photo.Id);
                }
            }

            return applicationUser.Id;
        }


        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserByIdAsync(string id)
        {
            if (id == null)
            {
                throw new SoftcodeArgumentMissingException("User not found");
            }
            if (id == LoggedInUser.UserId)
            {
                throw new SoftcodeArgumentMissingException("Logged In user cannot be deleted.");
            }

            var user = userManager.Users.Where(r => r.Id == id).FirstOrDefault();
            if (user == null)
            {
                throw new SoftcodeArgumentMissingException($"user: {user.UserName} not found");
            }

            if (user.ContactTypeId == ApplicationContactType.User)
            {
                //delete business profile entity                
                this.contactBusinessProfileRepository.Remove(this.contactBusinessProfileRepository.Where(t => t.ContactId == user.ContactId && t.EntityTypeId == ApplicationEntityType.BusinessProfile).ToList());
                this.contactRepository.Remove(this.contactRepository.Where(c => c.Id == user.ContactId).First());
                this.userBusinessProfileRepository.Remove(this.userBusinessProfileRepository.Where(t => t.UserId == user.Id).ToList());

            }
            //TODO: Check Access log for this user - proceed if no record found in access log
            await userManager.DeleteAsync(user);

            return true;

        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<bool> DeleteUsersAsync(List<string> ids)
        {
            if (ids == null)
            {
                throw new SoftcodeArgumentMissingException("User not found");
            }
            return Task.Run(() =>
            {
                foreach (string id in ids)
                {
                    var user = userManager.Users.Where(r => r.Id == id).FirstOrDefault();
                    if (user == null)
                    {
                        throw new SoftcodeArgumentMissingException($"user: {user.UserName} not found");
                    }

                    userManager.DeleteAsync(user);
                }
                return true;
            });
        }
        /// <summary>
        /// check whether this username exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ValidateUserName(string id, string username)
        {
            bool isValidUsername = true;

            //if it is not empty
            if (!string.IsNullOrEmpty(id))
            {
                ApplicationUser userObj = userManager.Users.Where(t => t.Id != id && t.UserName == username).FirstOrDefault();
                if (userObj != null)
                {
                    isValidUsername = false;
                }
            }
            else
            {
                ApplicationUser userObj = userManager.Users.Where(t => t.UserName == username).FirstOrDefault();
                if (userObj != null)
                {
                    isValidUsername = false;
                }
            }

            return isValidUsername;
        }
        /// <summary>
        /// check whether this email is used to another user or not
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateUserEmail(string id, string email)
        {
            bool isValidEmail = true;

            //if it is not empty
            if (!string.IsNullOrEmpty(id))
            {
                ApplicationUser userObj = userManager.Users.Where(t => t.Id != id && t.Email == email).FirstOrDefault();
                if (userObj != null)
                {
                    isValidEmail = false;
                }
            }
            else
            {
                ApplicationUser userObj = userManager.Users.Where(t => t.Email == email).FirstOrDefault();
                if (userObj != null)
                {
                    isValidEmail = false;
                }
            }

            return isValidEmail;
        }

        public async Task<bool> ChangeUserPasswordAsync(string currentPassword, string newPassword)
        {
            string userId = LoggedInUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new SoftcodeInvalidDataException("Invalid User");
            }

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                throw new SoftcodeArgumentMissingException("Current Password needed");
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new SoftcodeArgumentMissingException("New Password needed");
            }

            if (currentPassword.Equals(newPassword, StringComparison.InvariantCulture))
            {
                throw new SoftcodeArgumentMissingException("Current password and new password should not be same");
            }

            ApplicationUser applicationUser = await userManager.FindByIdAsync(userId);

            if (applicationUser == null)
            {
                throw new SoftcodeNotFoundException("User not found");
            }

            bool isPasswordMismatch = await userManager.CheckPasswordAsync(applicationUser, currentPassword);

            if (!isPasswordMismatch)
            {
                throw new SoftcodeInvalidDataException("Current password not match");
            }

            IdentityResult result = await userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);

            if (!result.Succeeded)
            {
                throw new SoftcodeInvalidDataException("Password not change");
            }

            return result.Succeeded;
        }
    }
}
