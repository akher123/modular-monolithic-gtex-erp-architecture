using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class State
    {
        public State()
        {
            CompanyOperatingState = new HashSet<CompanyOperatingState>();
            PostalCode = new HashSet<PostalCode>();
            PublicHoliday = new HashSet<PublicHoliday>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string StateName { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Country Country { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<CompanyOperatingState> CompanyOperatingState { get; set; }
        public ICollection<PostalCode> PostalCode { get; set; }
        public ICollection<PublicHoliday> PublicHoliday { get; set; }
    }
}
