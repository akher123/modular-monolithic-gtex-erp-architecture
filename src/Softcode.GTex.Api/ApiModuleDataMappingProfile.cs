
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiModuleDataMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiModuleDataMappingProfile()
        {
            CreateMap<BusinessProfile, BusinessProfileModel>();
            CreateMap<CustomCategory, CustomCategoryModel>();
            CreateMap<CustomCategoryMapType, CustomCategoryMapTypeModel>();
            CreateMap<CustomCategoryType, CustomCategoryTypeModel>();
            CreateMap<SecurityProfile, SecurityProfileModel>();
            CreateMap<SecurityConfiguration, SecurityConfigurationModel>();
            CreateMap<Contact, ContactModel>();
            CreateMap<Photo, PhotoModel>();
            CreateMap<ApplicationUser, ApplicationUserModel>();
            CreateMap<ApplicationMenu, ApplicationMenuModel>();
            CreateMap<ApplicationRole, RoleModel>();
            CreateMap<Communication, CommunicationModel>();

        }
    }
}
