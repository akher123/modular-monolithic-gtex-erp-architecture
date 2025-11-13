using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationPage : ITrackable
    {
        public ApplicationPage()
        {
            ApplicationMenus = new HashSet<ApplicationMenu>();
            ApplicationPageListFields = new HashSet<ApplicationPageListField>();
            ApplicationPageNavigations = new HashSet<ApplicationPageNavigation>();
            ApplicationPageServices = new HashSet<ApplicationPageService>();
            InverseParent = new HashSet<ApplicationPage>();
            ApplicationPageActions = new HashSet<ApplicationPageAction>();
            ApplicationPageDetailFields = new HashSet<ApplicationPageDetailField>();
            ApplicationPageGroups = new HashSet<ApplicationPageGroup>();
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
        public ICollection<ApplicationMenu> ApplicationMenus { get; set; }
        public ICollection<ApplicationPageAction> ApplicationPageActions { get; set; }
        public ICollection<ApplicationPageListField> ApplicationPageListFields { get; set; }
        public ICollection<ApplicationPageNavigation> ApplicationPageNavigations { get; set; }
        public ICollection<ApplicationPageService> ApplicationPageServices { get; set; }
        public ICollection<ApplicationPageDetailField> ApplicationPageDetailFields { get; set; }
        public ICollection<ApplicationPageGroup> ApplicationPageGroups { get; set; }

        public ICollection<ApplicationPage> InverseParent { get; set; }
    }
}
