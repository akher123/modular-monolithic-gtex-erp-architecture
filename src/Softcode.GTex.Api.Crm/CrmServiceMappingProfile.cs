
using AutoMapper;
using Softcode.GTex.ApplicationService.Crm.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api.Crm
{
    /// <summary>
    /// 
    /// </summary>
    public class CrmServiceMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public CrmServiceMappingProfile()
        {
            CreateMap<CompanyModel, Company>();
        }
    }
}
