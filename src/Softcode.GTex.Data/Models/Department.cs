using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    public abstract class Department : ITrackable
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
           
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        //public int? CompanyId { get; set; }
        //public int? BusinessProfileId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        //public BusinessProfile BusinessProfile { get; set; }
        //public Company Company { get; set; }
        public Contact CreatedByContact { get; set; }
        //public EntityType EntityType { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<Employee> Employees { get; set; }

      
        [NotMapped]
        public virtual int EntityId { get; set; }
    }
}
