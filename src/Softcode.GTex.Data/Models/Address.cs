using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    public abstract class Address : ITrackable
    {
        public Address()
        {
            BusinessProfileSites = new HashSet<BusinessProfileSite>();
        }
        public int Id { get; set; }
        public int AddressTypeId { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public int? StateId { get; set; }
        public string PostCode { get; set; }
        public int CountryId { get; set; }
        public byte[] TimeStamp { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; } = true;
        public int EntityTypeId { get; set; }


        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }
        
        public CustomCategory AddressType { get; set; }
        
        public Country Country { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        
        public State State { get; set; }

        public ICollection<BusinessProfileSite> BusinessProfileSites { get; set; }

        [NotMapped]
        public virtual int EntityId { get; set; }

        [NotMapped]
        public string DisplayAddress
        {
            get
            {
                return this.Street
               + (string.IsNullOrEmpty(this.Suburb) ? "" : ", " + this.Suburb) 
               + (this.State == null ? "" : ", " + this.State.StateName) 
               + (string.IsNullOrEmpty(this.PostCode) ? "" : " " + this.PostCode) ;
            }
        }
    }
}
