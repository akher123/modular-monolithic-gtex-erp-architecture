using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessCategory
    {
        public BusinessCategory()
        {
            BusinessCategoryMapTypes = new HashSet<BusinessCategoryMapType>();
            EmailJobQueueEmailTypes = new HashSet<EmailJobQueue>();
            
            EmailTemplates = new HashSet<EmailTemplate>();
            ServerAuthenticationTypes = new HashSet<EmailServer>();
            ServerProtocols = new HashSet<EmailServer>();
            ServerSenderOptions = new HashSet<EmailServer>();
            EmailQueues = new HashSet<EmailQueue>();
            EmailJobQueues = new HashSet<EmailJobQueue>();
            SecurityProfiles = new HashSet<SecurityProfile>();
        }

        public int Id { get; set; }
        public int BusinessCategoryTypeId { get; set; }
        public string Name { get; set; }
        public string ActionKey { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public bool IsInternal { get; set; }
        public int? RowNo { get; set; }

        public BusinessCategoryType BusinessCategoryType { get; set; }
        public ICollection<BusinessCategoryMapType> BusinessCategoryMapTypes { get; set; }
        public ICollection<EmailJobQueue> EmailJobQueueEmailTypes { get; set; }
        
        public ICollection<EmailTemplate> EmailTemplates { get; set; }
        public ICollection<EmailServer> ServerAuthenticationTypes { get; set; }
        public ICollection<EmailServer> ServerProtocols { get; set; }
        public ICollection<EmailServer> ServerSenderOptions { get; set; }
        public ICollection<EmailQueue> EmailQueues { get; set; }
        public ICollection<EmailJobQueue> EmailJobQueues { get; set; }
        public ICollection<SecurityProfile> SecurityProfiles { get; set; }
    }
}
