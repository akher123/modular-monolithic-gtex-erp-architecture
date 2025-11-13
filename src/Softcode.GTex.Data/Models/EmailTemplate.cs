using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EmailTemplate
    {
        public EmailTemplate()
        {
            EmailJobQueues = new HashSet<EmailJobQueue>();
        }

        public int Id { get; set; }
        public int BusinessMapTypeId { get; set; }
        public string Name { get; set; }
        public int BusinessProfileId { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string LayoutContent { get; set; }
        public int EmailServerId { get; set; }
        public bool UseLoggedInUserEmail { get; set; }
        public int Priority { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessCategory BusinessMapType { get; set; }
        public Contact CreatedByContact { get; set; }
        public EmailServer EmailServer { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<EmailJobQueue> EmailJobQueues { get; set; }
    }
}
