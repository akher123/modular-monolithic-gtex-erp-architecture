using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Models
{
    public class CommunicationViewModel
    {
        public CommunicationModel CommunicationModel { get; set; }
        public List<SelectModel> BusinessProfileSelectItems { get; set; }
        public List<SelectModel> CommunicationForSelectItems { get; set; }
        public List<SelectModel> CommunicationMathodSelectItems { get; set; }
        public List<SelectModel> CommunicationStatusSelectItems { get; set; }
        public int EntityTypeId
        {
            get
            {
                return ApplicationEntityType.Communication;
            }
        }

    }
}
