using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationPageNavigation
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string LinkName { get; set; }
        public string NavigateUrl { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ApplicationPage Page { get; set; }
    }
}
