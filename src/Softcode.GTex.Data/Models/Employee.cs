using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeCostCentres = new HashSet<EmployeeCostCentre>();
            EmployeeSites = new HashSet<EmployeeSite>();
            InverseSupervisors = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public int? EmploymentTypeId { get; set; }
        public DateTime? JobCommenceDate { get; set; }
        public DateTime? ProbationEndingDate { get; set; }
        public string JobDescription { get; set; }
        public string Floor { get; set; }
        public int? DepartmentId { get; set; }
        public string DeskId { get; set; }
        public int? SupervisorId { get; set; }
        public DateTime? JobCeasedDate { get; set; }
        public string JobCeasedReason { get; set; }
        public int? RegionId { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public int? BusinessUnitId { get; set; }
        public string ExternalPartnerId { get; set; }

        public BusinessUnit BusinessUnit { get; set; }
        public Department Department { get; set; }
        public CustomCategory EmploymentType { get; set; }
        public Contact Contact { get; set; }
        public Region Region { get; set; }
        public Employee Supervisor { get; set; }
        public ICollection<EmployeeCostCentre> EmployeeCostCentres { get; set; }
        public ICollection<EmployeeSite> EmployeeSites { get; set; }
        public ICollection<Employee> InverseSupervisors { get; set; }
    }
}
