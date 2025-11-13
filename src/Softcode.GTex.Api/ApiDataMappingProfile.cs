using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiDataMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiDataMappingProfile()
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
            
            CreateMap<Address, AddressModel>();
            CreateMap<ContactAddress, ContactAddressModel>()
                .ForMember(x => x.ContactId, c => c.MapFrom(m => m.ContactId))
                .ForMember(x => x.Id, c => c.MapFrom(m => m.Id))
                .ForMember(x => x.AddressTypeId, c => c.MapFrom(m => m.AddressTypeId))
                .ForMember(x => x.Street, c => c.MapFrom(m => m.Street))
                .ForMember(x => x.Suburb, c => c.MapFrom(m => m.Suburb))
                .ForMember(x => x.StateId, c => c.MapFrom(m => m.StateId))
                .ForMember(x => x.PostCode, c => c.MapFrom(m => m.PostCode))
                .ForMember(x => x.PostCode, c => c.MapFrom(m => m.PostCode))
                .ForMember(x => x.CountryId, c => c.MapFrom(m => m.CountryId))
                .ForMember(x => x.IsPrimary, c => c.MapFrom(m => m.IsPrimary))
                .ForMember(x => x.IsActive, c => c.MapFrom(m => m.IsActive))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
