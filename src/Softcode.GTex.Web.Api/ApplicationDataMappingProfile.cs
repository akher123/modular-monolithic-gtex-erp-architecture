using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Crm.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Web.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationDataMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDataMappingProfile()
        {
            CreateMap<Company, CompanyModel>();
        }
    }
}
