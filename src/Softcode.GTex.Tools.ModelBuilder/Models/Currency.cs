using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Country = new HashSet<Country>();
        }

        public int Id { get; set; }
        public string Isocode { get; set; }
        public string DisplayName { get; set; }
        public string Symbol { get; set; }
        public int Precision { get; set; }
        public decimal ExchangeRate { get; set; }
        public bool IsBaseCurrency { get; set; }
        public bool? IsActive { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<Country> Country { get; set; }
    }
}
