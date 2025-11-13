using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationMenu : ITrackable
    {
        public ApplicationMenu()
        {
            InverseParent = new HashSet<ApplicationMenu>();
        }
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public int RowNo { get; set; }
        public string HelpText { get; set; }
        public int? ParentId { get; set; }
        public int? PageId { get; set; }
        public string NavigateUrl { get; set; }
        public string ImageSource { get; set; }
        public int? EntityId { get; set; }
        public int? EntityRightId { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public SystemEntity Entity { get; set; }
        public ApplicationPage Page { get; set; }
        public SystemEntityRight EntityRight { get; set; }
        public ApplicationMenu Parent { get; set; }
        public ICollection<ApplicationMenu> InverseParent { get; set; }
    }
}
