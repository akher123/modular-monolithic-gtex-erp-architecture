using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Softcode.GTex.ApploicationService
{
    public class ApplicationServiceBuilder : IApplicationServiceBuilder
    {
        
        public ApplicationServiceBuilder(IHttpContextAccessor httpContextAccessor
            , IHostingEnvironment hostingEnvironment
            , IApplicationCacheService applicationCacheService
            , IPermissionService permissionService)
        {
            HttpContextAccessor = httpContextAccessor;
            HostingEnvironment = hostingEnvironment;
            ApplicationCacheService = applicationCacheService;
            PermissionService = permissionService;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public IApplicationCacheService ApplicationCacheService { get; }
         
        public IPermissionService PermissionService { get; }

    }
}
