using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Models
{
    public class SecurityConfigurationViewModel
    {
        public SecurityConfigurationModel SecurityConfiguration { get; set; }
        public List<SelectModel> AuthenticationTypeSelectItems { get; set; }
        public List<SelectModel> UserNameTypeSelectItems { get; set; }
    }
}
