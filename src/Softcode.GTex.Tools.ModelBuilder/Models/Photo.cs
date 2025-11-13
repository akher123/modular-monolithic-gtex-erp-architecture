using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Photo
    {
        public Photo()
        {
            BusinessProfile = new HashSet<BusinessProfile>();
            Company = new HashSet<Company>();
            Contact = new HashSet<Contact>();
            Region = new HashSet<Region>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string OrginalFileName { get; set; }
        public byte[] PhotoThumb { get; set; }
        public bool IsDefault { get; set; }
        public bool IsVisibleInPublicPortal { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
        public int? FileSizeInKb { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<BusinessProfile> BusinessProfile { get; set; }
        public ICollection<Company> Company { get; set; }
        public ICollection<Contact> Contact { get; set; }
        public ICollection<Region> Region { get; set; }
    }
}
