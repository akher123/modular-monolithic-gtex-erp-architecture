using AutoMapper;
using Softcode.GTex.ApplicationService.Hrm.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api.Hrm
{
    public class HrmServiceMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public HrmServiceMappingProfile()
        {
            CreateMap<EmployeeModel, Employee>();
         
        }
    }
}
