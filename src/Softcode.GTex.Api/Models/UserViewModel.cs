using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Models
{
    public class UserViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationUserModel User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ApplicationUserModel EmptyUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> BusinessProfileSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> TimeZoneSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> AuthenticationTypeSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> SecurityPolicySelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> TitleSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> UserTypeSelectItems { get; set; }
        /// <summary>
        /// get/set User Role Items
        /// </summary>
        public Task<List<TreeModel>> UserRoleSelectItems { get; set; }
        /// <summary>
        /// get/set User Right Items
        /// </summary>
        public Task<List<TreeModel>> UserRightSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool DisabledBusinessProfileSelection { get; set; } = false;

    }
}
