using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    public partial class Photo : ITrackable
    {
        public Photo()
        {
            BusinessProfiles = new HashSet<BusinessProfile>();
            Companies = new HashSet<Company>();
            Contacts = new HashSet<Contact>();
            Regions = new HashSet<Region>();
        }

        public int Id { get; set; }
        public byte[] PhotoThumb { get; set; }
        public bool IsDefault { get; set; }
        public bool IsVisibleInPublicPortal { get; set; }
        public int? DisplayOrder { get; set; }
        public string FileName { get; set; }
        public string OrginalFileName { get; set; }
        public string Description { get; set; }
        public int? FileSizeInKB { get; set; }

        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<BusinessProfile> BusinessProfiles { get; set; }
        public ICollection<Company> Companies { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Region> Regions { get; set; }
        [NotMapped]
        public string PhotoThumbnail { get; set; }
        [NotMapped]
        public string UploadedFileName { get; set; }
    }
}
