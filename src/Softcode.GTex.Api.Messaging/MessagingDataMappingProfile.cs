using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public class MessagingDataMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public MessagingDataMappingProfile()
        {
            //CreateMap<BusinessProfile, BusinessProfileModel>();
            CreateMap<EmailServer, EmailServerModel>();
        }
    }
}
