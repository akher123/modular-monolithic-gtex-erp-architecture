using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    public partial class EmailJobQueue :ITrackable
    {
        public EmailJobQueue()
        {
            EmailAttachments = new HashSet<EmailAttachment>();
            EmailQueues = new HashSet<EmailQueue>();
            EmailRecipients = new HashSet<EmailRecipient>();
        }

        public int Id { get; set; }
        public int EmailTypeId { get; set; }
        public int EntityId { get; set; }
        public int EntityTypeId { get; set; }
        public int EmailTemplateId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Subject { get; set; }
        public string LayoutContent { get; set; }
        public byte[] MappingObject { get; set; }
        public DateTime ExecutionTime { get; set; }
        public DateTime? LastExecutedOn { get; set; }
        public int NoOfAttempt { get; set; }

        public int StatusId { get; set; }
        public string ErrorText { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public int BusinessProfileId { get; set; }
        public string ScheduleNote { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
        public BusinessCategory EmailType { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        
        public BusinessCategory Status { get; set; }
        public ICollection<EmailAttachment> EmailAttachments { get; set; }
        public ICollection<EmailQueue> EmailQueues { get; set; }
        public ICollection<EmailRecipient> EmailRecipients { get; set; }
    }
}
