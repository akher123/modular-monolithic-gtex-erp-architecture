using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ContactModel ContactModel { get; set; }
        public ContactModel EmptyContactModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> BusinessProfileSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> CompanySelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> TitleSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> PositionSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> TimezoneSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> SkillsSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> ImTypeSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> GenderSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> PreferredContactMethodSelectItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectModel> PreferredPhoneTypeSelectItems { get; set; }        
    }
}
