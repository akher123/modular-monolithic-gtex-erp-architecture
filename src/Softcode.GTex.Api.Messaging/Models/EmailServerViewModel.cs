using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Messaging.Models
{
    public class EmailServerViewModel : BaseViewModel
    {
        public EmailServerModel EmailServer { get; set; }
        public List<SelectModel> BusinessProfileSelectItems { get; set; }
        public List<SelectModel> ProtocolSelectItems { get; set; }
        public List<SelectModel> SenderOptionSelectItems { get; set; }
        public List<SelectModel> AuthenticationTypeSelectItems { get; set; }
    }
}
