
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiModuleServiceMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiModuleServiceMappingProfile()
        {
         
            CreateMap<BusinessProfileModel, BusinessProfile>();
            CreateMap<CustomCategoryModel, CustomCategory>();
            CreateMap<SecurityProfileModel, SecurityProfile>().ForMember(x => x.TimeStamp, opt => opt.Ignore());
            CreateMap<SecurityConfigurationModel, SecurityConfiguration>();
            CreateMap<ContactModel, Contact>();
            CreateMap<PhotoModel, Photo>();
            CreateMap<ApplicationUserModel, ApplicationUser>();
            CreateMap<ApplicationMenuModel, ApplicationMenu>();
            CreateMap<RoleModel, ApplicationRole>().ForMember(x => x.RoleRights, opt => opt.Ignore());
            CreateMap<CommunicationModel, Communication>();

        }
    }
}
