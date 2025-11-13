using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Collections.Generic;
using Softcode.GTex.ExceptionHelper;

namespace Softcode.GTex.ApploicationService
{
    public abstract class BaseService<T> : IApplicationBase
        where T : class
    {
        private readonly IApplicationServiceBuilder applicationServiceBuilder;
        

        public BaseService(IApplicationServiceBuilder applicationServiceBuilder)
        {
            this.applicationServiceBuilder = applicationServiceBuilder;           
           
            
        }

        public ILoggedInUser LoggedInUser
        {
            get
            {
                if (!IsApplicationServiceUser)
                {
                    ILoggedInUserService loggedInUserService;
                    if (applicationServiceBuilder.HttpContextAccessor.HttpContext != null)
                    {
                        loggedInUserService = applicationServiceBuilder.HttpContextAccessor.HttpContext.RequestServices.GetService<ILoggedInUserService>() as ILoggedInUserService;
                    }
                    else
                    {
                        loggedInUserService = ApplicationDependencyResolver.Current.GetService<ILoggedInUserService>() as ILoggedInUserService;
                    }
                    return loggedInUserService.LoggedInUser;
                }
                else
                {
                    throw new SoftcodeNotFoundException("ApplicationServiceUser");
                }
            }
        }


        public bool IsApplicationServiceUser { get; set; } = false;

        public IMapper Mapper
        {
            get
            {
                if (applicationServiceBuilder.HttpContextAccessor.HttpContext != null)
                {
                    return applicationServiceBuilder.HttpContextAccessor.HttpContext.RequestServices.GetService<IMapper>() as IMapper;
                }

                return ApplicationDependencyResolver.Current.GetService(typeof(IMapper)) as IMapper;
            }
        }

        public ILogger Logger
        {
            get
            {
                if (applicationServiceBuilder.HttpContextAccessor.HttpContext != null)
                {
                    return applicationServiceBuilder.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(ILogger<T>)) as ILogger<T>;
                }

                return ApplicationDependencyResolver.Current.GetService(typeof(ILogger<T>)) as ILogger<T>;
            }
        }

        public HttpContext HttpContext
        {
            get
            {
                return applicationServiceBuilder.HttpContextAccessor.HttpContext;
            }
        }

        public IHostingEnvironment HostingEnvironment
        {
            get
            {
                return applicationServiceBuilder.HostingEnvironment;
            }
        }

        public IApplicationCacheService ApplicationCacheService
        {
            get
            {
                return applicationServiceBuilder.ApplicationCacheService;
            }
        }


        public IPermissionService Permission
        {
            get
            {
                this.applicationServiceBuilder.PermissionService.Initialize(this.LoggedInUser, this.ApplicationCacheService);
                return this.applicationServiceBuilder.PermissionService;
            }
        }

        public void ReloadLoggedInUserData()
        {
            if (!IsApplicationServiceUser)
            {
                ILoggedInUserService loggedInUserService;
                if (applicationServiceBuilder.HttpContextAccessor.HttpContext != null)
                {
                    loggedInUserService = applicationServiceBuilder.HttpContextAccessor.HttpContext.RequestServices.GetService<ILoggedInUserService>() as ILoggedInUserService;
                }
                else
                {
                    loggedInUserService = ApplicationDependencyResolver.Current.GetService<ILoggedInUserService>() as ILoggedInUserService;
                }

                loggedInUserService.ReloadLoggedInUserData();
            }
            else
            {
                throw new SoftcodeNotFoundException("ApplicationServiceUser");
            }
        }
    }
}
