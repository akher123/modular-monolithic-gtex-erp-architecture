using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Country : ITrackable
    {
        public Country()
        {
            Companies = new HashSet<Company>();
            PublicHolidays = new HashSet<PublicHoliday>();
            States = new HashSet<State>();
            Addresses = new HashSet<Address>();
            PostalCodes = new HashSet<PostalCode>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool HasState { get; set; }
        public int? CurrencyId { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Currency Currency { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<Company> Companies { get; set; }
        public ICollection<PublicHoliday> PublicHolidays { get; set; }
        public ICollection<State> States { get; set; }
        public ICollection<PostalCode> PostalCodes { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
