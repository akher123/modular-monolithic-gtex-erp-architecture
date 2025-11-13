using System;
using System.Collections.Generic;
using System.Linq;

namespace Softcode.GTex.ApploicationService
{
    public class PermissionService : IPermissionService
    {
        private ILoggedInUser loggedInUser;
        private IApplicationCacheService applicationCacheService;
        public PermissionService()
        {

        }

        public void Initialize(ILoggedInUser loggedInUser, IApplicationCacheService applicationCacheService)
        {
            this.loggedInUser = loggedInUser;
            this.applicationCacheService = applicationCacheService;
        }


        public List<int> DefaultBusinessProfileRights
        {
            get
            {
                return this.applicationCacheService.GetApplicationRoleRight()
                  .Where(x => this.loggedInUser.DefaultBusinessProfileRoleIds.Contains(x.RoleId))
                  .Select(x => x.RightId).Distinct().ToList();
            }
        }



        public bool IsValid(int rightId)
        {
            if (!this.loggedInUser.IsSuperAdmin)
            {
                return this.DefaultBusinessProfileRights.Any(x => x == rightId);                
            }
            else
            {
                return true;
            }
        }
    }
}
