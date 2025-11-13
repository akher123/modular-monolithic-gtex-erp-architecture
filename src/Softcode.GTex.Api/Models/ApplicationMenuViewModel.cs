using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Models
{
    class ApplicationMenuViewModel : BaseViewModel
    {
        
        public List<ApplicationMenuModel> ApplicationMenu { get; set; }
        public ApplicationHeaderModel ApplicationHeader { get; set; }

    }
}
