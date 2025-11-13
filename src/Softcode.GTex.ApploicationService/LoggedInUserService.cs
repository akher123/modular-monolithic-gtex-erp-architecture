using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace Softcode.GTex.ApploicationService
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        private readonly IMemoryCache memoryCache;

        private readonly object userLock = new object();

        ILoggedInUser loggedInUser;

        public LoggedInUserService(
            IHttpContextAccessor httpContextAccessor
            , UserManager<ApplicationUser> userManager
            , RoleManager<ApplicationRole> roleManager
            , IMemoryCache memoryCache)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.memoryCache = memoryCache;
            //this.LoggedInUser = this.GetLoggedInUser();

        }

        public ILoggedInUser LoggedInUser
        {
            get
            {
                if (!this.IsApplicationServiceUser)
                {
                    if (loggedInUser != null)
                        return loggedInUser;
                    else
                    {
                        lock (userLock)
                        {
                            return GetLoggedInUser();
                        }
                    }
                }
                else
                {
                    return GetApplicationServiceUser();
                }
            }
            //rivate set;
        }

        private ILoggedInUser GetApplicationServiceUser()
        {

            loggedInUser = GetUserFromCache("ApplicationServiceUserKey");

            if (loggedInUser != null)
            {
                return loggedInUser;
            }

            ApplicationUser user = userManager.Users.Where(x => x.ContactTypeId == Configuration.ApplicationContactType.ServiceUser)
                .Include(x => x.Contact)
                .Include(x => x.UserBusinessProfiles)
                .ThenInclude(x => x.BusinessProfile).FirstOrDefault();

            if (user == null)
            {
                throw new SoftcodeUnauthorizedException("Invalid ApplicationServiceUser");
            }

            if (user.Contact == null)
            {
                throw new SoftcodeUnauthorizedException("Contact not found for this ApplicationServiceUser");
            }


            string[] userRoleNames = userManager.GetRolesAsync(user).Result.ToArray();

            //int businessProfileId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ApplicationConstants.BUSSINESS_PROFILE_ID_CLAIM).Value.ToInt();
            int businessProfileId = user.Contact.ContactBusinessProfiles.FirstOrDefault().BusinessProfileId;
            loggedInUser = new LoggedInUser
            {
                UserId = user.Id,
                ContectId = user.ContactId,
                FullName = ($"{user.Contact.FirstName ?? ""} {user.Contact.MiddleName ?? ""} {user.Contact.LastName ?? ""}"),
                FirstName = user.Contact.FirstName,
                LastName = user.Contact.LastName,
                MiddleName = user.Contact.MiddleName,
                EmailAddress = user.Contact.Email,
                RoleIds = roleManager.Roles.Where(r => userRoleNames.Contains(r.Name)).Select(r => r.Id).ToArray(),
                RoleNames = userRoleNames,
                MobileNumber = user.Contact.Mobile,
                PhoneNumber = user.Contact.HomePhone,
                DefaultBusinessProfileId = businessProfileId,
                //TODO
                //Gender = (user.Contact.Gender ?? 1) == 1 ? "Male" : "Female",
                ImageSource = "",
                IsDefaultBusinessProfile = user.UserBusinessProfiles.Any(t => t.BusinessProfileId == businessProfileId && t.BusinessProfile.IsDefault),
                IsSuperAdmin = userRoleNames.Contains(ApplicationConstants.SuperAdmin)
            };

            SetUserToCache("ApplicationServiceUserKey", loggedInUser);
            return loggedInUser;
        }

        public bool IsApplicationServiceUser { get; set; } = false;

        private ILoggedInUser GetLoggedInUser()
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated || !httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            {
                throw new SoftcodeUnauthorizedException("User not Authenticated");
            }

            if (!httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == ApplicationConstants.BusinessProfileIdClaim))
            {
                throw new SoftcodeInvalidDataException("Business profile not found for this user");
            }

            string userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new SoftcodeUnauthorizedException("Unauthorized user");
            }

            loggedInUser = GetUserFromCache(userId);

            if (loggedInUser != null)
            {
                return loggedInUser;
            }

            ApplicationUser user = userManager.Users.Where(x => x.Id == userId)
                .Include(x => x.UserBusinessProfiles).ThenInclude(x => x.BusinessProfile)
                .Include(x => x.Contact).ThenInclude(x => x.ContactBusinessProfiles)//.ThenInclude(x=>x.BusinessProfile)                
                .FirstOrDefault();

            if (user.Contact == null)
            {
                throw new SoftcodeInvalidDataException("Contact not found for this user");
            }

            if (user == null)
            {
                throw new SoftcodeUnauthorizedException("Invalid user");
            }

            string[] userRoleNames = userManager.GetRolesAsync(user).Result.ToArray();

            int businessProfileId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ApplicationConstants.BusinessProfileIdClaim).Value.ToInt();

            List<ApplicationRole> userRoles = roleManager.Roles.Where(r => userRoleNames.Contains(r.Name)).ToList();
            List<ApplicationRole> businessProfileRoles = roleManager.Roles.Where(r => r.BusinessProfileId == null || r.BusinessProfileId == businessProfileId).ToList();
            string[] defaultBusinessProfileRoleHierarchyIds = GetLoggedInUserAllRoles(businessProfileRoles, userRoleNames);
            loggedInUser = new LoggedInUser
            {
                UserId = user.Id,
                ContectId = user.ContactId,
                FullName = ($"{user.Contact.FirstName ?? ""} {user.Contact.MiddleName ?? ""} {user.Contact.LastName ?? ""}"),
                FirstName = user.Contact.FirstName,
                LastName = user.Contact.LastName,
                MiddleName = user.Contact.MiddleName,
                EmailAddress = user.Contact.Email,
                RoleIds = userRoles.Select(r => r.Id).ToArray(),
                DefaultBusinessProfileRoleIds = userRoles.Where(r => r.BusinessProfileId == null || r.BusinessProfileId == businessProfileId).Select(r => r.Id).ToArray(),
                DefaultBusinessProfileRoleHierarchyIds = defaultBusinessProfileRoleHierarchyIds,
                RoleNames = userRoleNames,
                MobileNumber = user.Contact.Mobile,
                PhoneNumber = user.Contact.HomePhone,
                DefaultBusinessProfileId = businessProfileId,
                //TODO
                //Gender = (user.Contact.Gender ?? 1) == 1 ? "Male" : "Female",
                ImageSource = "",
                IsDefaultBusinessProfile = user.UserBusinessProfiles.Any(t => t.BusinessProfileId == businessProfileId && t.BusinessProfile.IsDefault),
                UserBusinessProfileIds = user.UserBusinessProfiles.Select(u => u.BusinessProfileId).ToArray(),
                ContactBusinessProfileIds = user.Contact.ContactBusinessProfiles.Select(u => u.BusinessProfileId).ToArray(),
                IsSuperAdmin = userRoleNames.Contains(ApplicationConstants.SuperAdmin)

            };

            SetUserToCache(userId, loggedInUser);
            return loggedInUser;
        }


        private string[] GetLoggedInUserAllRoles(List<ApplicationRole> roles, string[] userRoleNames)
        {
            List<ApplicationRole> parentRoles = roles.Where(x => userRoleNames.Contains(x.Name)).ToList();

            List<string> roleIds = new List<string>();

            foreach (ApplicationRole role in parentRoles)
            {
                if (!roleIds.Contains(role.Id))
                {
                    roleIds.Add(role.Id);
                    SetLoggedInUserAllChildRoles(roles, role.Id, roleIds);
                }
            }

            return roleIds.ToArray();
        }

        private void SetLoggedInUserAllChildRoles(List<ApplicationRole> roles, string parentRoleId, List<string> roleIds)
        {
            foreach (ApplicationRole role in roles.Where(x => x.ParentRoleId == parentRoleId))
            {
                if (!roleIds.Contains(role.Id))
                {
                    roleIds.Add(role.Id);
                    SetLoggedInUserAllChildRoles(roles, role.Id, roleIds);
                }
            }
        }


        //private List<ApplicationRoleRight> GetApplicationRoleRight()
        //{
        //    if (!memoryCache.TryGetValue(ApplicationConstants.CacheKey.ApplicationRoleRightKey, out List<ApplicationRoleRight> roleRight))
        //    {
        //        roleRight = this.roleManager.Roles.Include(r=>r.RoleRights).SelectMany(r=>r.RoleRights).ToList();
        //        memoryCache.Set(ApplicationConstants.CacheKey.ApplicationRoleRightKey, roleRight, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
        //    }
        //    return roleRight;
        //}

        private LoggedInUser GetUserFromCache(string userId)
        {
            memoryCache.TryGetValue(userId, out LoggedInUser user);
            return user;
        }

        private void SetUserToCache(string userId, ILoggedInUser user)
        {
            memoryCache.Set(userId, user, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
        }

        private MemoryCacheEntryOptions GetCacheOption(int timeoutMinute)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(timeoutMinute * 60) // caceh will expire if inactive for 5 seconds
            };
            return options;
        }

        public void ReloadLoggedInUserData()
        {
            this.loggedInUser = null;

            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                && httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            {
                string userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                if (string.IsNullOrWhiteSpace(userId))
                {
                    throw new SoftcodeUnauthorizedException("Unauthorized user");
                }

                SetUserToCache(userId, this.loggedInUser);
                GetLoggedInUser();
            }
        }
    }
}
