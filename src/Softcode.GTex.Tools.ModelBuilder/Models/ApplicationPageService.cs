using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationPageService
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string ServiceUrl { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ApplicationPage Page { get; set; }
    }
}
