using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class State : ITrackable
    {
        public State()
        {
            Companies = new HashSet<Company>();
            CompanyOperatingStates = new HashSet<CompanyOperatingState>();
            PostalCodes = new HashSet<PostalCode>();
            PublicHolidays = new HashSet<PublicHoliday>();
            Addresses = new HashSet<Address>();
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
        public ICollection<Company> Companies { get; set; }
        public ICollection<CompanyOperatingState> CompanyOperatingStates { get; set; }
        public ICollection<PostalCode> PostalCodes { get; set; }
        public ICollection<PublicHoliday> PublicHolidays { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
