using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [SecurityHeaders]
    [Route("api/system-settings/users")]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService userService;
        private readonly IBusinessProfileService businessProfileService;
        private readonly ICustomCategoryService customCategoryService;
        private readonly IAddressDatabaseService addressDatabaseService;
        private readonly ISystemConfigurationService systemConfig;
        private readonly ISecurityProfileService securityProfile;
        private readonly IRoleService roleService;
        private readonly IContactService contactService;
        private readonly IBusinessCategoryService businessCategory;
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="businessProfileService"></param>
        /// <param name="customCategoryService"></param>
        /// <param name="addressDatabaseService"></param>
        /// <param name="systemConfig"></param>
        /// <param name="securityProfile"></param>
        /// <param name="roleService"></param>
        /// <param name="contactService"></param>
        /// <param name="businessCategory"></param>
        public UserController(IUserService userService
            , IBusinessProfileService businessProfileService
            , ICustomCategoryService customCategoryService
            , IAddressDatabaseService addressDatabaseService
            , ISystemConfigurationService systemConfig
            , ISecurityProfileService securityProfile
            , IRoleService roleService
            , IContactService contactService
            , IBusinessCategoryService businessCategory
            )
        {
            this.userService = userService;
            this.businessProfileService = businessProfileService;
            this.customCategoryService = customCategoryService;
            this.addressDatabaseService = addressDatabaseService;
            this.systemConfig = systemConfig;
            this.securityProfile = securityProfile;
            this.roleService = roleService;
            this.contactService = contactService;
            this.businessCategory = businessCategory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-contact-select-items")]
        
        public async Task<IActionResult> GetcontactsAsync([FromBody] List<int> businessProfileIds)
        {
            return Ok(await contactService.GetContactSelectItemsForUserAsync(businessProfileIds));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-employee-select-items")]
        public async Task<IActionResult> GetEmployeesAsync([FromBody] List<int> businessProfileIds)
        {

            return Ok(await contactService.GetEmployeeSelectItemsForUserAsync(businessProfileIds));

        }

        /// <summary>
        /// get type and category list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-user-list")]
        [ActionAuthorize(ApplicationPermission.User.ShowUserList)]
        public async Task<IActionResult> GetUserList(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult> { Result = await userService.GetUserListAsync(loadOptions) });
        }
        /// <summary>
        /// Get user detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-user-detail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserDetailAsync(string id)
        {
            UserViewModel result = new UserViewModel
            {
                User = await userService.GetUserModelByIdAsync(id),
                EmptyUser = userService.GetUserModelByIdAsync(null).Result,
                IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile,
                BusinessProfileSelectItems = businessProfileService.GetContactBusinessProfileSelectItemsByUserBPIdsAsync().Result,
                TimeZoneSelectItems = addressDatabaseService.GetTimeZoneSelectItemsAsync().Result,
                AuthenticationTypeSelectItems = systemConfig.GetUserAuthenticationTypeSelectItems(),
                UserTypeSelectItems = businessCategory.GetBusinessCategoryByType(ApplicationBusinessCategoryType.ApplicationContactType),
                SecurityPolicySelectItems = securityProfile.GetSecurityProfileSelectItemsAsync().Result,
                //UserRoleSelectItems = roleService.GetActiveRolesAsync(),
                UserRightSelectItems = roleService.GetRightByRoleListAsync(id)
            };


            //result.TitleSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.TitleType).Result;
            result.DisabledBusinessProfileSelection = LoggedInUser.UserId == id || result.User.IsSuperAdmin;

            // Set Default items
            if (string.IsNullOrWhiteSpace(id))
            {  
                result.EmptyUser.BusinessProfileIds.Add(LoggedInUser.DefaultBusinessProfileId);                
                result.EmptyUser.IdentityType = -1;
                result.EmptyUser.SecurityProfileId = result.SecurityPolicySelectItems.FirstOrDefault(b => b.IsDefault)?.Id.ToInt();
                result.User = result.EmptyUser;
            }

            ResponseMessage<UserViewModel> response = new ResponseMessage<UserViewModel> { Result = result };

            return Ok(response);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="bps"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("get-select-box-data-for-user-by-bpids")]
        public async Task<IActionResult> GetSelectBoxDataForUser([FromQuery] int[] bps)
        {
            ResponseMessage<List<SelectModel>> response = new ResponseMessage<List<SelectModel>>();
            if (bps.Length == 1)
            {
                response.Result = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType, bps[0]).Result;
            }
            else if (bps.Length > 1)
            {
                response.Result = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType, bps).Result;
            }           
            else
            {
                response.Result = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType);
            }

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bps"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-user-role-and-title-data-by-bpids")]
        public async Task<IActionResult> GetUserRoleAndTitleDataBybpids([FromQuery] int[] bps, [FromQuery] string userId)
        {
           
            UserViewModel model = new UserViewModel();
            if (bps.Length == 1)
            {
                model.TitleSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType, bps[0]).Result;
            }
            else if (bps.Length > 1)
            {
                model.TitleSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType, bps).Result;
            }
            else
            {
                model.TitleSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType);
            }

            model.UserRoleSelectItems = roleService.GetActiveRolesByBPIdsAsync(userId, bps);

            ResponseMessage<UserViewModel> response = new ResponseMessage<UserViewModel> {
                Result = model
            };

            return Ok(response);
        }


        /// <summary>
        /// get contact detail for user creation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-contact-for-user-detail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetContactDetailForUserCreationAsync(int id)
        {
            ContactViewModel result = new ContactViewModel
            {
                ContactModel = await contactService.GetContactForUserByContactIdAsync(id)
            };

            ResponseMessage<ContactViewModel> response = new ResponseMessage<ContactViewModel> { Result = result };
            return Ok(response);
        }
        /// <summary>
        /// insert user information
        /// </summary>        
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert-user-detail")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.User.CreateUser)]
        public async Task<IActionResult> SaveUserDetail([FromBody] ApplicationUserModel userModel)
        {
            userModel.BusinessProfileIds = new List<int>() { userModel.BusinessProfileId };//For UserBusinessProfile Maintain

            ResponseMessage<string> response = new ResponseMessage<string>
            {
                Result = await userService.InsertUserDetailAsync(userModel)
            };
            return Ok(response);
        }
        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-user-detail/{id}")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.User.UpdateUser)]
        public async Task<IActionResult> UpdateUserDetail(string id, [FromBody] ApplicationUserModel userModel)
        {
            userModel.BusinessProfileIds = new List<int>() { userModel.BusinessProfileId };//For UserBusinessProfile Maintain
            ResponseMessage<string> response = new ResponseMessage<string>
            {
                Result = await userService.UpdateUserDetailAsync(id, userModel)
            };
            return Ok(response);
        }
        /// <summary>
        /// get configuration menu
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-user-photo")]
        [ActionAuthorize(ApplicationPermission.User.UpdateUser)]
        public IActionResult UploadUserPhoto()
        {
            ResponseMessage<string> response = new ResponseMessage<string>();

            try
            {
                IFormFile file = Request.Form.Files[0];
                string folderName = "images";
                string webRootPath = HostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                response.Result = "Upload Successful.";
                return Ok(response);
            }
            catch (System.Exception)
            {
                response.Result = "Image cannot be uploaded.";
                return Ok(response);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-user/{id}")]
        [ActionAuthorize(ApplicationPermission.User.DeleteUser)]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            return Ok(await userService.DeleteUserByIdAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-users")]
        [ActionAuthorize(ApplicationPermission.User.DeleteUser)]
        public async Task<IActionResult> DeleteUsersAsync([FromBody] List<string> ids)
        {
            return Ok(await userService.DeleteUsersAsync(ids));
        }

    

    }
}
