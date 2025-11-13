using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationPage
    {
        public ApplicationPage()
        {
            ApplicationMenu = new HashSet<ApplicationMenu>();
            ApplicationPageFieldDetail = new HashSet<ApplicationPageFieldDetail>();
            ApplicationPageNavigation = new HashSet<ApplicationPageNavigation>();
            ApplicationPageService = new HashSet<ApplicationPageService>();
            InverseParent = new HashSet<ApplicationPage>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string RoutingUrl { get; set; }
        public string Title { get; set; }
        public string PageType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ApplicationPage Parent { get; set; }
        public ICollection<ApplicationMenu> ApplicationMenu { get; set; }
        public ICollection<ApplicationPageFieldDetail> ApplicationPageFieldDetail { get; set; }
        public ICollection<ApplicationPageNavigation> ApplicationPageNavigation { get; set; }
        public ICollection<ApplicationPageService> ApplicationPageService { get; set; }
        public ICollection<ApplicationPage> InverseParent { get; set; }
    }
}
