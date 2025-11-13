using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Region : ITrackable
    {
        public Region()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string RegionName { get; set; }
        public string Description { get; set; }
        public int? LogoId { get; set; }
        public int SortOrder { get; set; }
        public byte[] TimeStamp { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public int? BusinessProfileId { get; set; }
        public string RegionCode { get; set; }
        public string ExternalPartnerId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public Photo Logo { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}
