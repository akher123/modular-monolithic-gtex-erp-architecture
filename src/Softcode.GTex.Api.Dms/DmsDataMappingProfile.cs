using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Dms.Models;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Api.Dms
{
    public class DmsDataMappingProfile : MappingProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public DmsDataMappingProfile()
        {
            CreateMap<DocumentMetadata, DocumentMetadataModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x=>x.files, opt=>opt.Ignore());

            CreateMap<DocumentFileStore, DocumentFileStoreModel>();
        }
    }
}
