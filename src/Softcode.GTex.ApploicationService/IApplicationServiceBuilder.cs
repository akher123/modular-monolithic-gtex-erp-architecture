using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Softcode.GTex.ApploicationService
{
    public interface IApplicationServiceBuilder
    {
        IHttpContextAccessor HttpContextAccessor { get; }

        IHostingEnvironment HostingEnvironment { get;}

        IApplicationCacheService ApplicationCacheService { get; }
        IPermissionService PermissionService { get; }

    }
}
