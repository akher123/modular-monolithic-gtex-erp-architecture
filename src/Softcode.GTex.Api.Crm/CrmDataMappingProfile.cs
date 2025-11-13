using AutoMapper;
using Softcode.GTex.ApplicationService.Crm.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api.Crm
{
    /// <summary>
    /// 
    /// </summary>
    public class CrmDataMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public CrmDataMappingProfile()
        {
            CreateMap<Company, CompanyModel>();
        }
    }
}
