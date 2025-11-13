using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IRoleService
    {        
        Task<LoadResult> GetRolesAsync(DataSourceLoadOptionsBase options);
      
        Task<RoleModel> GetRoleModelByIdAsync(string id);
        //List<SelectModel> RoleSelectItems();
      
        Task<string> UpdateRoleAsync(string id, RoleModel model);
        Task<string> CreateRoleAsync(RoleModel model);
        Task<LoadResult> GetUserListByRoleAsync(string roleId, DataSourceLoadOptionsBase options);
        Task<List<UserSelectModel>> GetUnassignUserListAsync(int businessProfileId, string roleId);

        Task<List<TreeModel>> GetUserPermissionsAsync(string parentRoleId);
        Task<List<TreeModel>> GetActiveRolesAsync();

        Task<List<TreeModel>> GetActiveRolesByBPIdsAsync(string userId,int[] BPIds);
        Task<List<TreeModel>> GetPermissionsByRoleListAsync(List<string> roleIds);
        Task<List<TreeModel>> RoleTreeListByBPIdsAndParentRoleIdsAsync(int businessProfileId);
        Task<List<TreeModel>> RoleTreeListByBpIdAndParentRoleIdAsync(int businessProfileId, string parentRoleId);
        Task<List<TreeModel>> GetRightByRoleListAsync(string id);

        List<string> GetUserSelectedRolesByUserAsync();
        
        List<string> GetRoleNameList(List<string> ids);
        
        Task<bool> AssignRoleToUsersAsync(string roleId, List<string> userIds);
        Task<bool> RemoveUserFromRoleAsync(string roleId, string userId);
        bool IsRightExistAsync(int rightId);
        Task<bool> DeleteRoleAsync(string id);
        Task<bool> DeleteRolesAsync(List<string> ids);
    }
}