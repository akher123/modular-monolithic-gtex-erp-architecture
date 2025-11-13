using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.ApplicationService.Hrm.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            Contact = new ContactModel();
            this.EmployeeCostCentreIds = new List<int>();
            this.EmployeeSiteIds = new List<int>();
        }
        public int Id { get; set; }
                
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "Maximum length of employee id is 50 characters.") ]
        public string EmployeeId { get; set; }
        public int? EmploymentTypeId { get; set; }
        public DateTime? JobCommenceDate { get; set; }
        public DateTime? ProbationEndingDate { get; set; }
        
        [MaxLength(400, ErrorMessage = "Maximum length of job description is 400 characters.")]
        public string JobDescription { get; set; }
        
        [MaxLength(100, ErrorMessage = "Maximum length of floor is 100 characters.")]
        public string Floor { get; set; }
        public int? DepartmentId { get; set; }
       
        [MaxLength(50, ErrorMessage = "Maximum length of desk id is 50 characters.")]
        public string DeskId { get; set; }
        public int? SupervisorId { get; set; }
        public DateTime? JobCeasedDate { get; set; }
        
        [MaxLength(400, ErrorMessage = "Maximum length of job ceased reason is 400 characters.")]
        public string JobCeasedReason { get; set; }
        public int? RegionId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsArchived { get; set; }
        public int? BusinessUnitId { get; set; }
        [Display(Name = "External Partner Id")]
        [MaxLength(50, ErrorMessage = "Maximum length of external partner id is 50 characters.")]
        public string ExternalPartnerId { get; set; }
        public ContactModel Contact { get; set; }
        public List<int> EmployeeCostCentreIds { get; set; }
        public List<int> EmployeeSiteIds { get; set; }

    }
}
