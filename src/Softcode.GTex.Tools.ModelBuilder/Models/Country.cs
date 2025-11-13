using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            PublicHoliday = new HashSet<PublicHoliday>();
            State = new HashSet<State>();
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
        public ICollection<PublicHoliday> PublicHoliday { get; set; }
        public ICollection<State> State { get; set; }
    }
}
