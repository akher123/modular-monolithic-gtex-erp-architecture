using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessProfileSite
    {
        public BusinessProfileSite()
        {
            EmployeeSites = new HashSet<EmployeeSite>();
        }

        public int Id { get; set; }
        public int BusinessProfileId { get; set; }
        public string SiteName { get; set; }
        public int? AddressId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? PrimaryContactId { get; set; }
        public int? SiteMapDocumentId { get; set; }
        public bool IsActive { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Address Address { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public Contact PrimaryContact { get; set; }
        public DocumentMetadata SiteMapDocument { get; set; }
        public ICollection<EmployeeSite> EmployeeSites { get; set; }
    }
}
