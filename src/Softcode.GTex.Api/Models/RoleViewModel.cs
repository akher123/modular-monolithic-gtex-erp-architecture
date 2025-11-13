using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Models
{
    public class RoleViewModel : BaseViewModel
    {
        public RoleModel Role { get; set; }
        public RoleModel EmptyRole { get; set; }
        public List<SelectModel> BusinessProfileSelectItems { get; set; }
        public List<SelectModel> RoleSelectItems { get; set; }
    }
}
