using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EmailQueue
    {
        public int Id { get; set; }
        public int EmailJobQueueId { get; set; }        
        public string Subject { get; set; }
        public string Body { get; set; }
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
        public string ErrorText { get; set; }
        public DateTime? SentDateTime { get; set; }        
        public int StatusId { get; set; }
        public bool IsArchived { get; set; } = false;
        public int Priority { get; set; }
        public int FailedAttemptCount { get; set; }
        public EmailJobQueue EmailJobQueue { get; set; }
        public BusinessCategory Status { get; set; }
    }
}
