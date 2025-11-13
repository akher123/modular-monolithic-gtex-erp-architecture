using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IApplicationCacheService
    {
        List<ApplicationPage> GetApplicationListPages();
        void ClearApplicationListPage();
        List<ApplicationMenu> GetApplicationMenu();
        void ClearApplicationMenu();
        List<ApplicationRoleRight> GetApplicationRoleRight();
        void ClearApplicationRoleRight();
        ApplicationRole GetSuperAdminRole();
        void ClearSuperAdminRole();
        List<BusinessCategoryType> GetBusinessCategoryType();
        void ClearBusinessCategoryType();

        List<CustomCategoryType> GetCustomCategoryType();
        void ClearCustomCategoryType();

        List<EntityType> GetEntityType();
        void ClearEntityType();

       
         
        void RemoveItemByKey(string key);
        void SetItemByKey(string key, object value, int timeoutMinute = 0);
        object GetItemByKey(string key);
    }
}
