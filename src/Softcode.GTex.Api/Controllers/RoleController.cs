using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [SecurityHeaders]
    [Route("api/system-settings/roles")]
    public class RoleController : BaseController<RoleController>
    {
        private readonly IRoleService roleService;
        private readonly IBusinessProfileService businessProfileService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleService"></param>
        /// <param name="businessProfileService"></param>
        public RoleController(IRoleService roleService
            , IBusinessProfileService businessProfileService)
        {
            this.roleService = roleService;
            this.businessProfileService = businessProfileService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-role-list")]
        [ActionAuthorize(ApplicationPermission.Role.ShowRolesList)]
        public async Task<IActionResult> GetRoleList(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult> { Result = await roleService.GetRolesAsync(loadOptions) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-role")]
        [ActionAuthorize(ApplicationPermission.Role.ViewRolesDetails)]
        public async Task<IActionResult> GetRole(string id)
        {
            return Ok(new ResponseMessage<RoleViewModel>
            {
                Result = new RoleViewModel
                {
                    Role = await roleService.GetRoleModelByIdAsync(id),
                    EmptyRole = roleService.GetRoleModelByIdAsync(null).Result,
                    BusinessProfileSelectItems = businessProfileService.GetUserBusinessProfileSelectItemsAsync().Result,
                    IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-role")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Role.CreateRole)]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleModel model)
        {
            return Ok(new ResponseMessage<string> { Result = await roleService.CreateRoleAsync(model) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-role/{id}")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Role.UpdateRole)]
        public async Task<IActionResult> UpdateRoleAsync(string id, [FromBody] RoleModel model)
        {
            return Ok(new ResponseMessage<string> { Result = await roleService.UpdateRoleAsync(id, model) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-role/{id}")]
        [ActionAuthorize(ApplicationPermission.Role.DeleteRole)]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            return Ok(await roleService.DeleteRoleAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-roles")]
        [ActionAuthorize(ApplicationPermission.Role.DeleteRole)]
        public async Task<IActionResult> DeleteRolesAsync([FromBody] List<string> ids)
        {
            return Ok(await roleService.DeleteRolesAsync(ids));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-role-tree-list-by-bp/{businessProfileId:int}")]
        public async Task<IActionResult> RoleTreeListByBusinessProfileAsync(int businessProfileId)
        {
            return Ok(new ResponseMessage<List<TreeModel>>
            {
                Result = await roleService.RoleTreeListByBPIdsAndParentRoleIdsAsync(businessProfileId)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <param name="parentRoleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-role-tree-list-for-copy-option/{businessProfileId:int}/{parentRoleId}")]
        public async Task<IActionResult> RoleTreeListByBusinessProfileAndParentRoleAsync(int businessProfileId, string parentRoleId)
        {
            return Ok(new ResponseMessage<List<TreeModel>>
            {
                Result = await roleService.RoleTreeListByBpIdAndParentRoleIdAsync(businessProfileId, parentRoleId)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("unassign-user-list/{businessProfileId:int}/{roleId}")]
        public async Task<IActionResult> GetUnassignUserListAsync(int businessProfileId, string roleId)
        {
            return Ok(new ResponseMessage<List<UserSelectModel>>
            {
                Result = await roleService.GetUnassignUserListAsync(businessProfileId, roleId)
            });
        }

        /// <summary>
        /// get type and category list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-user-role-list/{roleId}")]
        public async Task<IActionResult> GetUserListByRoleAsync(string roleId, DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult> { Result = await roleService.GetUserListByRoleAsync(roleId, loadOptions) });
        }

        /// <summary>
        /// get type and category list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-user-permission-list/{parentRoleId}")]
        public async Task<IActionResult> GetUserPermissionList(string parentRoleId)
        {
            return Ok(new ResponseMessage<List<TreeModel>> { Result = await roleService.GetUserPermissionsAsync(parentRoleId) });
        }

        /// <summary>
        /// get type and category list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("get-role-right-list")]
        public async Task<IActionResult> GetRightListByRoleList([FromBody] List<string> ids)
        {
            return Ok(new ResponseMessage<List<TreeModel>> { Result = await roleService.GetPermissionsByRoleListAsync(ids) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-active-role-list")]
        public async Task<IActionResult> GetActiveRoleList()
        {
            return Ok(new ResponseMessage<List<TreeModel>> { Result = await roleService.GetActiveRolesAsync() });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("assign-role-to-users/{roleId}")]
        public async Task<IActionResult> AssignRoleToUsers(string roleId, [FromBody] List<string> userIds)
        {
            return Ok(new ResponseMessage<bool> { Result = await roleService.AssignRoleToUsersAsync(roleId, userIds) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove-user-from-role/{roleId}/{userId}")]
        public async Task<IActionResult> RemoveUserFromRole(string roleId, string userId)
        {
            return Ok(new ResponseMessage<bool> { Result = await roleService.RemoveUserFromRoleAsync(roleId, userId) });
        }
    }
}
