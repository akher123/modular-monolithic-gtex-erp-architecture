using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Softcode.GTex.ApploicationService
{
    public interface IApplicationBase : ILoggedInUserService
    {
        IMapper Mapper { get; }

        ILogger Logger { get; }

        HttpContext HttpContext { get; }

        IHostingEnvironment HostingEnvironment { get; }

        IApplicationCacheService ApplicationCacheService { get; }

        IPermissionService Permission { get; }

        void ReloadLoggedInUserData();
    }
}
