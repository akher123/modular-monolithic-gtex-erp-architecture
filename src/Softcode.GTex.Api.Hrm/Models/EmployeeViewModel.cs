using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Hrm.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Hrm.Models
{
    public class EmployeeViewModel :BaseViewModel
    {
        public EmployeeModel Employee { get; set; }
        public EmployeeModel EmptyEmployee { get; set; }
        public List<SelectModel> ImTypeSelectItems { get;  set; }
        public List<SelectModel> BusinessProfileSelectItems { get;  set; }
        public List<SelectModel> TitleSelectItems { get;  set; }
        public List<SelectModel> PositionSelectItems { get;  set; }
        public List<SelectModel> TimezoneSelectItems { get;  set; }
        public List<SelectModel> SkillsSelectItems { get;  set; }
        public List<SelectModel> GenderSelectItems { get;  set; }
        public List<SelectModel> PreferredContactMethodSelectItems { get;  set; }
        public List<SelectModel> BusinessUnitSelectItems { get;  set; }
        public List<SelectModel> RegionSelectItems { get;  set; }
        public List<SelectModel> EmploymentTypeSelectItems { get;  set; }
        public List<SelectModel> DepartmentSelectItems { get;  set; }
        public List<SelectModel> PreferredPhoneTypeSelectItems { get;  set; }
        //public List<SelectModel> CostCentreSelectItems { get; set; }
    }
}
