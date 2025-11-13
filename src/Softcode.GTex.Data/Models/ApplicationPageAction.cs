using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationPageAction: ITrackable
    {
        public int Id { get; set; }
        public int? PageId { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string NavigateUrl { get; set; }
        public string ActionName { get; set; }
        public int? SortOrder { get; set; }
        public int? RightId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ApplicationPage Page { get; set; }
    }
}
