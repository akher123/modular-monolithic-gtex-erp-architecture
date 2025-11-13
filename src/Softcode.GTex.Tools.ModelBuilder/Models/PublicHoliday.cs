using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class PublicHoliday
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public string EventName { get; set; }
        public byte[] Timestamp { get; set; }
        public int? BusinessProfileId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Country Country { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public State State { get; set; }
    }
}
