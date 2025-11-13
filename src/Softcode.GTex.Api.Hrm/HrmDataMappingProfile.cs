using AutoMapper;
using Softcode.GTex.ApplicationService.Hrm.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api.Hrm
{
    public class HrmDataMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public HrmDataMappingProfile()
        {
            CreateMap<Employee , EmployeeModel>();
        }
    }
}
