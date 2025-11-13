using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessUnit : ITrackable
    {
        public BusinessUnit()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public int BusinessProfileId { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
