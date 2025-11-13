using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softcode.GTex.ExceptionHelper;

namespace Softcode.GTex.ApploicationService
{
    public class RoleService : BaseService<RoleService>, IRoleService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<IdentityUserRole<string>> userRoleRepository;
        private readonly IRepository<SystemModule> moduleRepository;
        private readonly IRepository<ApplicationRoleRight> applicationRoleRightRepository;
        private readonly IRepository<SystemEntityRight> entityRightRepository;
        private readonly IRepository<SystemEntity> entityRepository;
        private readonly IRepository<UserBusinessProfile> userBusinessProfileRepository;
        private readonly IRepository<ContactBusinessProfile> contactBusinessProfileRepository;

        public RoleService(IApplicationServiceBuilder applicationServiceBuilder
            , RoleManager<ApplicationRole> roleManager
            , UserManager<ApplicationUser> userManager
            , IRepository<IdentityUserRole<string>> userRoleRepository
            , IRepository<SystemModule> moduleRepository
            , IRepository<ApplicationRoleRight> applicationRoleRightRepository
            , IRepository<SystemEntityRight> entityRightRepository
            , IRepository<SystemEntity> entityRepository
            , IRepository<UserBusinessProfile> userBusinessProfileRepository
            , IRepository<ContactBusinessProfile> contactBusinessProfileRepository)
            : base(applicationServiceBuilder)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userRoleRepository = userRoleRepository;
            this.moduleRepository = moduleRepository;
            this.applicationRoleRightRepository = applicationRoleRightRepository;
            this.entityRightRepository = entityRightRepository;
            this.entityRepository = entityRepository;
            this.userBusinessProfileRepository = userBusinessProfileRepository;
            this.contactBusinessProfileRepository = contactBusinessProfileRepository;
        }

        public async Task<LoadResult> GetRolesAsync(DataSourceLoadOptionsBase options)
        {
            options.Select = new[] { "Id", "Name", "Description", "IsActive", "BusinessProfile.CompanyName", "ParentRole.Name" };

            if (LoggedInUser.IsSuperAdmin)
            {
                return await Task.Run(() => DataSourceLoader.Load(roleManager.Roles.Where(x => x.Name != ApplicationConstants.SuperAdmin), options));
            }
            
            int businessProfileId = LoggedInUser.DefaultBusinessProfileId;
            string[] roles = LoggedInUser.DefaultBusinessProfileRoleHierarchyIds;

            List<ApplicationRole> query = roleManager.Roles
                .Where(x => x.Name != ApplicationConstants.SuperAdmin && x.BusinessProfileId == businessProfileId &&  roles.Contains(x.Id))
                .Include(x=>x.BusinessProfile).ToList();

            return await Task.Run(() => DataSourceLoader.Load(query, options));
        }

        public async Task<RoleModel> GetRoleModelByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new RoleModel
                {
                    BusinessProfileId = LoggedInUser.DefaultBusinessProfileId
                };
            }

            ApplicationRole applicationRole = await roleManager.Roles.Include(x => x.RoleRights).FirstOrDefaultAsync(x => x.Id.Equals(id, StringComparison.InvariantCulture));

            if (applicationRole == null)
            {
                throw new SoftcodeNotFoundException("Role not found");
            }

            RoleModel model = Mapper.Map<RoleModel>(applicationRole);
            model.RoleRights = applicationRole.RoleRights.Select(x => x.RightId).ToArray();

            return model;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleAsync(string id)
        {
            if (id == null)
            {
                throw new SoftcodeArgumentMissingException("Role not found");
            }


            ApplicationRole role = roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
            if (role == null)
            {
                throw new SoftcodeArgumentMissingException($"Role: {role.Name} not found");
            }

            if (userRoleRepository.Exists(x => x.RoleId == id))
            {
                throw new SoftcodeInvalidDataException($"The role ({role.Name}) cannot be deleted because it is associated with system user(s).");
            }

            applicationRoleRightRepository.Remove(applicationRoleRightRepository.Where(x => x.RoleId == id).ToList());
            var result = await roleManager.DeleteAsync(role);
            this.ApplicationCacheService.ClearApplicationRoleRight();
            this.ReloadLoggedInUserData();
            return result.Succeeded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRolesAsync(List<string> ids)
        {
            if (ids == null)
            {
                throw new SoftcodeArgumentMissingException("Role not found");
            }

            foreach (string id in ids)
            {
                ApplicationRole role = roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
                if (role == null)
                {
                    throw new SoftcodeArgumentMissingException($"Role: {role.Name} not found");
                }

                await roleManager.DeleteAsync(role);
            }
            this.ReloadLoggedInUserData();
            return true;
        }

        public bool IsRightExistAsync(int rightId)
        {
            string[] roleIds = LoggedInUser.RoleIds;

            if (roleIds.Count() < 1)
            {
                return false;
            }

            return Permission.IsValid(rightId);
        }

        //public List<SelectModel> RoleSelectItems()
        //{
        //    if (LoggedInUser.IsSuperAdmin)
        //    {
        //        return roleManager.Roles.Where(x => x.IsActive && x.ParentRoleId != null)
        //        .Select(x => new SelectModel { Id = x.Id.ToString(), Name = x.Name }).ToList();
        //    }

        //    string[] roleIds = LoggedInUser.RoleIds;
        //    return roleManager.Roles.Where(x => x.IsActive && (roleIds.Contains(x.Id) || roleIds.Contains(x.ParentRoleId)))
        //        .Select(x => new SelectModel { Id = x.Id.ToString(), Name = x.Name }).ToList();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public async Task<List<TreeModel>> RoleTreeListByBPIdsAndParentRoleIdsAsync(int businessProfileId)
        {
           
            List<ApplicationRole> roles = await Task.Run(() => roleManager.Roles.Where(x => x.IsActive && (x.BusinessProfileId == null
                              || x.BusinessProfileId.Value == businessProfileId))
                             .Include(x => x.BusinessProfile)
                             .OrderBy(x => x.Name).ToList());

            List<ApplicationRole> parentRoles = new List<ApplicationRole>();
            if (!LoggedInUser.IsSuperAdmin)
            {
                string[] roleIds = this.LoggedInUser.DefaultBusinessProfileRoleHierarchyIds;

                parentRoles = roles.Where(x => x.Name != ApplicationConstants.SuperAdmin && !roles.Where(a => roleIds.Contains(a.Id)).Select(r => r.Id).Contains(x.ParentRoleId)).ToList();
            }
            else
            {
                parentRoles = roles.Where(x => !roles.Select(r => r.Id).Contains(x.ParentRoleId)).ToList();
            }

            List<TreeModel> result = new List<TreeModel>();
            foreach (var item in parentRoles)
            {
                result.Add(new TreeModel { SId = item.Id, Name = item.Name, IsExpanded = true });
                AddChildRole(roles, item.Id, result);
            }
            return result;
        }

        public async Task<List<TreeModel>> RoleTreeListByBpIdAndParentRoleIdAsync(int businessProfileId, string roleId)
        {
            string[] roleIds = LoggedInUser.RoleIds;

            List<ApplicationRole> roles = await Task.Run(() => roleManager.Roles.Where(x => x.IsActive
                                                         && (x.BusinessProfileId == null || x.BusinessProfileId.Value == businessProfileId))
                                                .Include(x => x.BusinessProfile)
                                                .OrderBy(x => x.Name).ToList());

            List<ApplicationRole> parentRoles = roles.Where(x => x.ParentRoleId == roleId).ToList();

            List<TreeModel> result = new List<TreeModel>();
            foreach (var item in parentRoles)
            {
                if (item.ParentRoleId == null)
                {
                    AddChildRole(roles, item.Id, result);
                }
                else
                {
                    result.Add(new TreeModel { SId = item.Id, Name = item.Name, IsExpanded = true });
                    AddChildRole(roles, item.Id, result);
                }

            }
            return result;
        }

        private void AddChildRole(IEnumerable<ApplicationRole> childRoles, string parentId, List<TreeModel> result)
        {
            bool isRoot = result.Count == 0;

            foreach (var item in childRoles.Where(y => y.ParentRoleId == parentId))
            {
                if (!isRoot)
                {
                    result.Add(new TreeModel { SId = item.Id, SParentId = item.ParentRoleId, Name = item.Name, IsExpanded = true });
                }
                else
                {
                    result.Add(new TreeModel { SId = item.Id, Name = item.Name, IsExpanded = true });
                }
                AddChildRole(childRoles, item.Id, result);
            }
        }

        public async Task<string> CreateRoleAsync(RoleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new SoftcodeArgumentMissingException("Name is missing");
            }

            if (roleManager.RoleExistsAsync(model.Name).Result)
            {
                throw new SoftcodeInvalidDataException("Role name already exist");
            }

            ApplicationRole applicationRole = Mapper.Map<ApplicationRole>(model);
            applicationRole.CreatedByContactId = LoggedInUser.ContectId;
            applicationRole.CreatedDateTime = DateTime.Now;

            int[] rightIds = null;

            if (!string.IsNullOrWhiteSpace(model.CopyRoleId))
            {
                rightIds = roleManager.Roles.Include(x => x.RoleRights).FirstOrDefaultAsync(x => x.Id.Equals(model.CopyRoleId, StringComparison.InvariantCulture)).Result.RoleRights.Select(x => x.RightId).ToArray();
                applicationRole.RoleRights = rightIds.Select(x => new ApplicationRoleRight { RightId = x }).ToList();
            }

            await Task.Run(() => roleManager.CreateAsync(applicationRole));
            this.ApplicationCacheService.ClearApplicationRoleRight();
            this.ReloadLoggedInUserData();
            return applicationRole.Id;
        }

        public async Task<string> UpdateRoleAsync(string id, RoleModel model)
        {
            ApplicationRole applicationRole = await roleManager.Roles.Include(x => x.RoleRights).FirstOrDefaultAsync(x => x.Id.Equals(id, StringComparison.InvariantCulture));

            if (applicationRole == null)
            {
                throw new SoftcodeNotFoundException("Role not found");
            }

            if (model.Name != applicationRole.Name)
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    throw new SoftcodeArgumentMissingException("Name is missing");
                }

                if (roleManager.RoleExistsAsync(model.Name).Result)
                {
                    throw new SoftcodeInvalidDataException("Role name already exist");
                }
            }

            applicationRole = Mapper.Map(model, applicationRole);
            applicationRole.LastUpdatedByContactId = LoggedInUser.ContectId;
            applicationRole.LastUpdatedDateTime = DateTime.Now;

            while (applicationRole.RoleRights.Count != 0)
            {
                applicationRole.RoleRights.Remove(applicationRole.RoleRights.FirstOrDefault());
            }

            applicationRole.RoleRights = model.RoleRights.Select(x => new ApplicationRoleRight { RightId = x }).ToList();

            await Task.Run(() => roleManager.UpdateAsync(applicationRole));
            this.ApplicationCacheService.ClearApplicationRoleRight();
            return applicationRole.Id;
        }

        public async Task<LoadResult> GetUserListByRoleAsync(string roleId, DataSourceLoadOptionsBase options)
        {
            string[] userIds = userRoleRepository.Where(x => x.RoleId == roleId).Select(x => x.UserId).ToArray();
            var usersQuery = userManager.Users.Where(x => userIds.Contains(x.Id)).Include(x => x.Contact)
                .Select(x => new
                {
                    x.Id,
                    Username = x.UserName,
                    x.Contact.DisplayName,
                    Active = x.IsActive
                });

            //options.Select = new[] { "Id", "Contact.DisplayName", "IsActive" };
            return await Task.Run(() => DataSourceLoader.Load(usersQuery, options));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<UserSelectModel>> GetUnassignUserListAsync(int businessProfileId, string roleId)
        {
            string[] userIds = userRoleRepository.Where(x => x.RoleId == roleId).Select(x => x.UserId).ToArray();
            string loggedInUserId = this.LoggedInUser.UserId;
            List<UserSelectModel> usersQuery = await Task.Run(() => userBusinessProfileRepository.Where(b => b.BusinessProfileId == businessProfileId)
                .Include(b => b.User).ThenInclude(b => b.Contact)
                .Where(u => u.UserId != loggedInUserId && !userIds.Contains(u.User.Id))
                .Select(x => new UserSelectModel
                {
                    Id = x.User.Id,
                    Name = x.User.UserName + " (" + x.User.Contact.DisplayName + ")"
                }).ToList());

            return usersQuery;
        }


        public async Task<List<TreeModel>> GetUserPermissionsAsync(string parentRoleId)
        {
            if (string.IsNullOrWhiteSpace(parentRoleId))
            {
                return null;
            }

            List<RoleRight> query = new List<RoleRight>();

            if (roleManager.Roles.Any(x => x.Id == parentRoleId && x.Name == ApplicationConstants.SuperAdmin))
            {
                query = entityRightRepository.AsQueryable().Include(y => y.SystemEntity).ThenInclude(z => z.Module).ToList()
                     .Select(x => new RoleRight
                     {
                         RightId = x.Id,
                         RightName = x.Name,
                         EntityId = x.SystemEntity.Id,
                         EntityName = x.SystemEntity.EntityName,
                         ModuleId = x.SystemEntity.Module.ModuleId,
                         ModuleName = x.SystemEntity.Module.ModuleName
                     }).ToList();
            }
            else
            {
                query = applicationRoleRightRepository.Where(x => x.RoleId == parentRoleId).Include(x => x.SystemEntityRight).ThenInclude(y => y.SystemEntity).ThenInclude(z => z.Module).ToList()
                     .Select(x => new RoleRight
                     {
                         RightId = x.RightId,
                         RightName = x.SystemEntityRight.Name,
                         EntityId = x.SystemEntityRight.SystemEntity.Id,
                         EntityName = x.SystemEntityRight.SystemEntity.EntityName,
                         ModuleId = x.SystemEntityRight.SystemEntity.Module.ModuleId,
                         ModuleName = x.SystemEntityRight.SystemEntity.Module.ModuleName
                     }).ToList();
            }
            var modules = query.Select(x => new
            {
                x.ModuleId,
                x.ModuleName
            }).Distinct();

            var entities = query.Select(x => new
            {
                x.ModuleId,
                x.ModuleName,
                x.EntityId,
                x.EntityName
            }).Distinct();

            List<TreeModel> treeModels = modules.Select(x => new TreeModel
            {
                Id = x.ModuleId,
                Name = x.ModuleName,
                Items = entities.Where(y => y.ModuleId == x.ModuleId).Select(z => new TreeModel
                {
                    Id = z.EntityId,
                    Name = z.EntityName,
                    Items = query.Where(o => o.EntityId == z.EntityId).Select(k => new TreeModel
                    {
                        Id = k.RightId,
                        ParentId = z.EntityId,
                        Name = k.RightName
                    }).ToList()
                }).ToList()
            }).ToList();

            return treeModels;

        }
        /// <summary>
        /// Get active role 
        /// </summary>
        /// <returns></returns>
        public async Task<List<TreeModel>> GetActiveRolesAsync()
        {
            List<ApplicationRole> query = roleManager.Roles.Where(x => x.IsActive && x.ParentRoleId == null).ToList();
            List<TreeModel> treeModels = query.Select(x => new TreeModel
            {
                SId = x.Id,
                Name = x.Name,
                Items = roleManager.Roles.Where(y => y.ParentRoleId == x.Id).Select(z => new TreeModel
                {
                    SId = z.Id,
                    SParentId = x.Id,
                    Name = z.Name

                }).ToList()
            }).ToList();

            return treeModels;
        }

        //private string[] GetLoggedInUserAllRoles(List<ApplicationRole> roles)
        //{
        //    List<ApplicationRole> parentRoles = roles.Where(x => LoggedInUser.DefaultBusinessProfileRoleIds.Contains(x.Id)).ToList();

        //    List<string> roleIds = new List<string>();

        //    foreach (ApplicationRole role in parentRoles)
        //    {
        //        if (!roleIds.Contains(role.Id))
        //        {
        //            roleIds.Add(role.Id);
        //            SetLoggedInUserAllChildRoles(roles, role.Id, roleIds);
        //        }
        //    }

        //    return roleIds.ToArray();
        //}

        //private void SetLoggedInUserAllChildRoles(List<ApplicationRole> roles, string parentRoleId, List<string> roleIds)
        //{
        //    foreach (ApplicationRole role in roles.Where(x=>x.ParentRoleId==parentRoleId))
        //    {
        //        if (!roleIds.Contains(role.Id))
        //        {

        //            roleIds.Add(role.Id);
        //            SetLoggedInUserAllChildRoles(roles, role.Id, roleIds);
        //        }
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bpIds"></param>
        /// <returns></returns>
        public async Task<List<TreeModel>> GetActiveRolesByBPIdsAsync(string userId, int[] bpIds)
        {


            List<ApplicationRole> roles = roleManager.Roles.Where(x => x.IsActive && (x.BusinessProfileId == null || bpIds.Contains(x.BusinessProfileId.Value)))
                                        .Include(x => x.BusinessProfile)
                                        .OrderBy(x => x.Name).ToList();

            string[] loggedInUserRoles = this.LoggedInUser.DefaultBusinessProfileRoleHierarchyIds;

            string[] userRoles = userRoleRepository.Where(x => x.UserId == userId).Select(x => x.RoleId).ToArray();

            string[] selectedRoles = loggedInUserRoles.Union(userRoles).Distinct().ToArray();




            bool isSuperAdminUser = userRoles.Any(x => x == this.ApplicationCacheService.GetSuperAdminRole().Id);
            bool isDisabled = isSuperAdminUser || userId == this.LoggedInUser.UserId;



            if (!LoggedInUser.IsSuperAdmin && !isSuperAdminUser)
            {
                roles = roles.Where(x => selectedRoles.Contains(x.Id)).ToList();
            }



            List<ApplicationRole> parentRoles = roles.Where(x => x.ParentRoleId == null || !roles.Select(r => r.Id).Contains(x.ParentRoleId)).ToList();

            if (LoggedInUser.IsSuperAdmin && !isSuperAdminUser)
            {
                parentRoles = parentRoles.SelectMany(x => x.InverseParentRole).ToList();
            }

            List<TreeModel> treeModels = parentRoles.Select(x => new TreeModel
            {
                SId = x.Id,
                Name = x.Name + (x.BusinessProfileId > 0 ? " (BP: " + x.BusinessProfile.CompanyName + ")" : ""),
                SParentId = x.ParentRoleId,
                Disabled = isDisabled || (!loggedInUserRoles.Any(r => r == x.Id) && !LoggedInUser.IsSuperAdmin),
                IsExpanded = true,
                Visible = true,
                Items = GetChildRoles(roles, x.Id, loggedInUserRoles, isDisabled)
            }).ToList();

            return treeModels;
        }


        private List<TreeModel> GetChildRoles(IEnumerable<ApplicationRole> childRoles, string parentId, string[] loggedInUserRoles, bool isDisabled)
        {
            var childs = childRoles
                    .Where(x => x.ParentRoleId == parentId)
                    .Select(x => new TreeModel
                    {
                        SId = x.Id,
                        Name = x.Name,
                        SParentId = x.ParentRoleId,
                        Disabled = isDisabled || (!loggedInUserRoles.Any(r => r == x.Id) && !LoggedInUser.IsSuperAdmin),
                        IsExpanded = true,
                        Visible = false,
                        Items = GetChildRoles(childRoles, x.Id, loggedInUserRoles, isDisabled)

                    })
                    .ToList();

            return childs;
        }

        /// <summary>
        /// Get active role 
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserSelectedRolesByUserAsync()
        {
            return roleManager.Roles.Where(x => LoggedInUser.RoleIds.Contains(x.Id)).Select(t => t.Id).ToList();
        }
        /// <summary>
        /// Get active role 
        /// </summary>
        /// <returns></returns>
        public List<string> GetRoleNameList(List<string> ids)
        {
            List<ApplicationRole> applicationRoles = roleManager.Roles.Where(x => ids.Contains(x.Id)).ToList();
            return applicationRoles.Select(t => t.Name).ToList();
        }
        /// <summary>
        /// get user permission by role id list
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<List<TreeModel>> GetPermissionsByRoleListAsync(List<string> roleIds)
        {
            List<int> rightIds = this.applicationRoleRightRepository.Where(t => roleIds.Contains(t.RoleId)).Select(t => t.RightId).Distinct().ToList();
            List<int> entityIds = this.entityRightRepository.Where(t => rightIds.Contains(t.Id)).Select(t => t.EntityId).Distinct().ToList();
            List<int> moduleIds = this.entityRepository.Where(t => entityIds.Contains(t.Id)).Select(t => t.ModuleId).Distinct().ToList();
            List<SystemModule> query = moduleRepository.Where(x => x.IsActive).Include(y => y.SystemEntities).ThenInclude(z => z.SystemEntityRights).ToList();

            List<TreeModel> treeModels = query.Where(t => moduleIds.Contains(t.ModuleId)).Select(x => new TreeModel
            {
                Id = x.ModuleId,
                Name = x.ModuleName,
                Items = x.SystemEntities.Where(t => entityIds.Contains(t.Id)).Select(y => new TreeModel
                {
                    Id = y.Id,
                    Name = y.EntityName,
                    Items = y.SystemEntityRights.Where(t => rightIds.Contains(t.Id)).Select(z => new TreeModel
                    {
                        Id = z.Id,
                        ParentId = y.Id,
                        Name = z.Name
                    }).ToList()
                }).ToList()
            }).ToList();



            return treeModels;
        }

        /// <summary>
        /// get user permission by role id list
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<List<TreeModel>> GetRightByRoleListAsync(string id)
        {
            ApplicationUser user = await userManager.Users.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return new List<TreeModel>();
            }

            var roleIds = await userManager.GetRolesAsync(user);
            List<int> rightIds = this.applicationRoleRightRepository.Where(t => roleIds.Contains(t.RoleId)).Select(t => t.RightId).Distinct().ToList();
            List<int> entityIds = this.entityRightRepository.Where(t => rightIds.Contains(t.Id)).Select(t => t.EntityId).Distinct().ToList();
            List<int> moduleIds = this.entityRepository.Where(t => entityIds.Contains(t.Id)).Select(t => t.ModuleId).Distinct().ToList();
            List<SystemModule> query = moduleRepository.Where(x => x.IsActive).Include(y => y.SystemEntities).ThenInclude(z => z.SystemEntityRights).ToList();

            List<TreeModel> treeModels = query.Where(t => moduleIds.Contains(t.ModuleId)).Select(x => new TreeModel
            {
                Id = x.ModuleId,
                Name = x.ModuleName,
                Items = x.SystemEntities.Where(t => entityIds.Contains(t.Id)).Select(y => new TreeModel
                {
                    Id = y.Id,
                    Name = y.EntityName,
                    Items = y.SystemEntityRights.Where(t => rightIds.Contains(t.Id)).Select(z => new TreeModel
                    {
                        Id = z.Id,
                        ParentId = y.Id,
                        Name = z.Name
                    }).ToList()
                }).ToList()
            }).ToList();



            return treeModels;
        }

        public Task<string> SaveRoleAsync(string id, RoleModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AssignRoleToUsersAsync(string roleId, List<string> userIds)
        {
            foreach (string userId in userIds)
            {
                userRoleRepository.AddEntity(new IdentityUserRole<string> { RoleId = roleId, UserId = userId });
            }

            int count = await userRoleRepository.SaveChangesAsync();
            return count > 0;
        }

        public async Task<bool> RemoveUserFromRoleAsync(string roleId, string userId)
        {
            var userRole = await userRoleRepository.FindOneAsync(x => x.UserId == userId && x.RoleId == roleId);

            return userRoleRepository.Delete(userRole) > 0;
        }
    }
}
