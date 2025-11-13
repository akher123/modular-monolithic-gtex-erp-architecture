using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiServiceMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiServiceMappingProfile()
        {
            CreateMap<BusinessProfileModel, BusinessProfile>();
            CreateMap<CustomCategoryModel, CustomCategory>();
            CreateMap<SecurityProfileModel, SecurityProfile>().ForMember(x=>x.TimeStamp, opt=>opt.Ignore());
            CreateMap<SecurityConfigurationModel, SecurityConfiguration>();
            CreateMap<ContactModel, Contact>();            
            CreateMap<PhotoModel, Photo > ();
            CreateMap<ApplicationUserModel, ApplicationUser>();
            CreateMap<ApplicationMenuModel, ApplicationMenu>();
            CreateMap<RoleModel, ApplicationRole>().ForMember(x=>x.RoleRights, opt => opt.Ignore());
            CreateMap<CommunicationModel, Communication>();
            
            CreateMap<AddressModel, Address>();
            CreateMap<ContactAddressModel, ContactAddress>()
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
