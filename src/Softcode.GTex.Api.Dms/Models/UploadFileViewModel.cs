using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Dms.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Dms.Models
{
    public class UploadFileViewModel: BaseViewModel
    {
        public DocumentMetadataModel DocumentMetadata { get; set; }
        public List<SelectModel> BusinessProfileSelectItems { get; set; }
        public List<SelectModel> DocumentTypeSelectItems { get; set; }
        public List<SelectModel> DocumentForSelectItems { get; set; }

        public List<AttachedFileModel> AttachedFiles { get; set; }


    }
}
