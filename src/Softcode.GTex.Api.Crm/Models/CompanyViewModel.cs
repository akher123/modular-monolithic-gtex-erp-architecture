using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Crm.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Crm.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyViewModel: BaseViewModel
    {
        public CompanyModel Company { get; set; }
        public CompanyModel EmptyCompany { get; set; }
        public List<SelectModel> BusinessProfileSelectItems { get; set; }
        public List<SelectModel> CounrtySelectItems { get; set; }
        public List<SelectModel> StateSelectItems { get; set; }
        public List<SelectModel> IndustrySelectItems { get; set; }
        public List<SelectModel> OrganisationTypeSelectItems { get; set; }
        public List<SelectModel> RatingSelectItems { get; set; }
        public List<SelectModel> PreferredContactMethodSelectItems { get; set; }
        public List<SelectModel> RelationshipTypeSelectItems { get; set; }
    }
}
