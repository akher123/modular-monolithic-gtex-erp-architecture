using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EmployeeCostCentre
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CostCentreId { get; set; }

        public CostCentre CostCentre { get; set; }
        public Employee Employee { get; set; }
    }
}
