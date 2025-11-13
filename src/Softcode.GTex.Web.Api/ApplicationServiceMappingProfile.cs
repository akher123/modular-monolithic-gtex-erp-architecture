using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Crm.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Web.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationServiceMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationServiceMappingProfile()
        {
            CreateMap<CompanyModel, Company>();
          
        }
    }
}
