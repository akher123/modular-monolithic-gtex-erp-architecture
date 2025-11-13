using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Models
{
    public class SecurityProfileViewModel
    {
        public SecurityProfileModel SecurityProfile { get; set; }
        public List<SelectModel> PasswordCombinationTypeSelectItems { get; set; }
    }
}
