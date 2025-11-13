using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public class MessagingServiceMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public MessagingServiceMappingProfile()
        {
            //CreateMap<BusinessProfileModel, BusinessProfile>();
            CreateMap<EmailServerModel, EmailServer>();

        }
    }
}
