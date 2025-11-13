using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class TimeZone
    {
        public TimeZone()
        {
            Contacts = new HashSet<Contact>();
        }

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public int OffsetValueInMinutes { get; set; }
        public bool SupportsDaylightSavings { get; set; }
        public DateTime? DaylightSavingStartDateTime { get; set; }
        public DateTime? DaylightSavingEndDateTime { get; set; }
        public bool IsDaylight { get; set; }
        public string DaylightDisplayName { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
