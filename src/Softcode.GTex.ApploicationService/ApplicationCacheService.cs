using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Softcode.GTex.ApploicationService
{
    public class ApplicationCacheService : IApplicationCacheService
    {
        private readonly IRepository<ApplicationPage> applicationPageRepository;
        private readonly IRepository<ApplicationRoleRight> applicationRoleRightRepository;
        private readonly IRepository<ApplicationRole> applicationRoleRepository;
        private readonly IRepository<ApplicationMenu> applicationMenuRepository;
        private readonly IRepository<BusinessCategoryType> businessCategoryTypeRepository;
        private readonly IRepository<CustomCategoryType> customCategoryTypeRepository;
        private readonly IRepository<EntityType> entityTypeRepository;
        private readonly IMemoryCache memoryCache;

        
        private readonly object menuLock = new object();
        private readonly object roleRightLock = new object();
        private readonly object saLock = new object();
        private readonly object ccLock = new object();
        private readonly object bcLock = new object();
        private readonly object entityLock = new object();
        private readonly object listPageLock = new object();

        public ApplicationCacheService(
            IRepository<ApplicationPage> applicationPageRepository,
            IRepository<ApplicationRoleRight> applicationRoleRightRepository, 
            IRepository<ApplicationMenu> applicationMenuRepository, 
            IRepository<BusinessCategoryType> businessCategoryTypeRepository,
            IRepository<CustomCategoryType> customCategoryTypeRepository,
            IRepository<EntityType> entityTypeRepository,
            IRepository<ApplicationRole> applicationRoleRepository,
            IMemoryCache memoryCache
            )
        {
            this.applicationPageRepository = applicationPageRepository;
            this.applicationMenuRepository = applicationMenuRepository;
            this.applicationRoleRightRepository = applicationRoleRightRepository;
            this.businessCategoryTypeRepository = businessCategoryTypeRepository;
            this.customCategoryTypeRepository = customCategoryTypeRepository;
            this.entityTypeRepository = entityTypeRepository;
            this.applicationRoleRepository = applicationRoleRepository;
            this.memoryCache = memoryCache;
        }

        public List<ApplicationPage> GetApplicationListPages()
        {
            lock (listPageLock)
            {
                if (!memoryCache.TryGetValue(ApplicationConstants.CacheKey.ApplicationListPageKey, out List<ApplicationPage> appMenu))
                {
                    appMenu = this.applicationPageRepository.GetAll()
                                .Include(p => p.ApplicationPageListFields)
                                .Include(p => p.ApplicationPageServices)
                                .Include(p => p.ApplicationPageNavigations).ToList();
                    memoryCache.Set(ApplicationConstants.CacheKey.ApplicationListPageKey, appMenu, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
                }
                return appMenu;
            }
        }


        public void ClearApplicationListPage()
        {
            memoryCache.Remove(ApplicationConstants.CacheKey.ApplicationListPageKey);
        }

        public List<ApplicationMenu> GetApplicationMenu()
        {
            lock (menuLock)
            {
                if (!memoryCache.TryGetValue(ApplicationConstants.CacheKey.ApplicationMenuKey, out List<ApplicationMenu> appMenu))
                {
                    appMenu = this.applicationMenuRepository.WhereAsync(m => m.IsVisible).Result.ToList();
                    memoryCache.Set(ApplicationConstants.CacheKey.ApplicationMenuKey, appMenu, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
                }
                return appMenu;
            }
        }


        public void ClearApplicationMenu()
        {
            memoryCache.Remove(ApplicationConstants.CacheKey.ApplicationMenuKey);
        }

        public List<ApplicationRoleRight> GetApplicationRoleRight()
        {
            lock (roleRightLock)
            {
                if (!memoryCache.TryGetValue(ApplicationConstants.CacheKey.ApplicationRoleRightKey, out List<ApplicationRoleRight> roleRight))
                {

                    roleRight = this.applicationRoleRightRepository.GetAllAsync().Result.ToList();
                    memoryCache.Set(ApplicationConstants.CacheKey.ApplicationRoleRightKey, roleRight, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
                }
                return roleRight;
            }
        }
        public void ClearApplicationRoleRight()
        {
            memoryCache.Remove(ApplicationConstants.CacheKey.ApplicationRoleRightKey);
        }
        public ApplicationRole GetSuperAdminRole()
        {
            lock (saLock)
            {
                if (!memoryCache.TryGetValue(ApplicationConstants.CacheKey.SuperAdminApplicationRoleKey, out ApplicationRole roleRight))
                {

                    roleRight = this.applicationRoleRepository.Where(x => x.ParentRoleId == null).First();
                    memoryCache.Set(ApplicationConstants.CacheKey.SuperAdminApplicationRoleKey, roleRight, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
                }
                return roleRight;
            }
        }

        public void ClearSuperAdminRole()
        {
            memoryCache.Remove(ApplicationConstants.CacheKey.SuperAdminApplicationRoleKey);
        }

        public List<CustomCategoryType> GetCustomCategoryType() {
            lock (ccLock)
            {
                if (!memoryCache.TryGetValue(ApplicationConstants.CacheKey.CustomCategoryTypeKey, out List<CustomCategoryType> customCategoryType))
                {

                    customCategoryType = this.customCategoryTypeRepository.GetAll().Include(x => x.CustomCategories).ToList();
                    memoryCache.Set(ApplicationConstants.CacheKey.CustomCategoryTypeKey, customCategoryType, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
                }
                return customCategoryType;
            }
        }
        public void ClearCustomCategoryType() {
            memoryCache.Remove(ApplicationConstants.CacheKey.CustomCategoryTypeKey);
        }

        public List<BusinessCategoryType> GetBusinessCategoryType()
        {
            lock (bcLock)
            {
                if (!memoryCache.TryGetValue(ApplicationConstants.CacheKey.BusinessCategoryTypetKey, out List<BusinessCategoryType> businessCategoryType))
                {

                    businessCategoryType = this.businessCategoryTypeRepository.GetAll().Include(x => x.BusinessCategories).ToList();
                    memoryCache.Set(ApplicationConstants.CacheKey.BusinessCategoryTypetKey, businessCategoryType, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
                }
                return businessCategoryType;
            }
        }

        public void ClearBusinessCategoryType()
        {
            memoryCache.Remove(ApplicationConstants.CacheKey.BusinessCategoryTypetKey);
        }


        public List<EntityType> GetEntityType()
        {
            lock (entityLock)
            {
                if (!memoryCache.TryGetValue(ApplicationConstants.CacheKey.EntityTypetKey, out List<EntityType> entityType))
                {

                    entityType = this.entityTypeRepository.GetAll().ToList();
                    memoryCache.Set(ApplicationConstants.CacheKey.EntityTypetKey, entityType, GetCacheOption(ItmConfigurations.DefaultCacheTimeout));
                }
                return entityType;
            }
        }

        public void ClearEntityType()
        {
            memoryCache.Remove(ApplicationConstants.CacheKey.EntityTypetKey);
        }


        private MemoryCacheEntryOptions GetCacheOption(int timeoutMinute)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
            {
                //AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(300), // cache will expire in 25 seconds
                SlidingExpiration = TimeSpan.FromSeconds(timeoutMinute * 60) // caceh will expire if inactive for 5 seconds
            };
            return options;
        }


        public void RemoveItemByKey(string key)
        {
            memoryCache.Remove(key);
        }

        public void SetItemByKey(string key, object value, int timeoutMinute=0)
        {
            if (timeoutMinute == 0)
            {
                timeoutMinute = ItmConfigurations.DefaultCacheTimeout;
            }

            memoryCache.Set(key, value, GetCacheOption(timeoutMinute));
        }

        public object GetItemByKey(string key)
        {
            object retValue;
            memoryCache.TryGetValue(key, out retValue);
            return retValue;
        }

       
    }
}
