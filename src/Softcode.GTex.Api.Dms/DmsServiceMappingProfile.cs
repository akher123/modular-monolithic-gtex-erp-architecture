using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Dms.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api.Dms
{
    public class DmsServiceMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public DmsServiceMappingProfile()
        {
            CreateMap<DocumentMetadataModel, DocumentMetadata>()
                .ForMember(x => x.TimeStamp, opt => opt.Ignore());
            CreateMap<DocumentFileStoreModel, DocumentFileStore>();
        }

    }
}
