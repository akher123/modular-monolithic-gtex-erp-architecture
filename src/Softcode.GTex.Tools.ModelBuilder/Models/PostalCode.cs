using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class PostalCode
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string DeliveryOffice { get; set; }
        public string PareclZone { get; set; }
        public string Bspnumber { get; set; }
        public string Bspname { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public State State { get; set; }
    }
}
