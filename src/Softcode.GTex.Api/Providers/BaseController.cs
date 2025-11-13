using System.Collections.Generic;
using AutoMapper;
using Softcode.GTex.ApploicationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Softcode.GTex.Api.Providers
{
    [ValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase, IApplicationBase
        where T : class
    {
        public ILoggedInUser LoggedInUser
        {
            get
            {
                ILoggedInUserService loggedInUserService = HttpContext.RequestServices.GetService<ILoggedInUserService>() as ILoggedInUserService;
                return loggedInUserService.LoggedInUser;
            }
        }

        public bool IsApplicationServiceUser { get; set; } = false;

        public IMapper Mapper
        {
            get
            {
                return HttpContext.RequestServices.GetService(typeof(IMapper)) as IMapper;
            }
        }

        public ILogger Logger
        {
            get
            {
                return HttpContext.RequestServices.GetService(typeof(ILogger<T>)) as ILogger<T>;
            }
        }

        public IHostingEnvironment HostingEnvironment
        {
            get
            {
                return HttpContext.RequestServices.GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;
            }
        }

        public IApplicationCacheService ApplicationCacheService => this.ApplicationCacheService;

        public IPermissionService Permission
        {
            get
            {
                throw new System.Exception();
            }
        }

        [HttpGet]
        [Route("ReloadLoggedInUserData")]
        [AllowAnonymous]
        public void ReloadLoggedInUserData()
        {
            throw new System.Exception();
        }
    }
}
