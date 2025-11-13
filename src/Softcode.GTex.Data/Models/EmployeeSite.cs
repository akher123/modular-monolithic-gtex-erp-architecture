using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EmployeeSite
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SiteId { get; set; }

        public Employee Employee { get; set; }
        public BusinessProfileSite BusinessProfileSite { get; set; }
    }
}
