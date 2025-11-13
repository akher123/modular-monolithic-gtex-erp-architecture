using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging.Models
{
    public class EmailJobQueueModel
    { 
        public int Id { get; set; }
        public int EmailTypeId { get; set; }
        public int EntityId { get; set; }
        public int EntityTypeId { get; set; }
        public int EmailTemplateId { get; set; }
        public int EmailTemplateMapTypeId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Subject { get; set; }
        public string LayoutContent { get; set; }
        public DateTime ExecutionTime { get; set; }
        public int NoOfAttempt { get; set; }
        public int BusinessProfileId { get; set; }
        public List<EmailMappingModel> EmailMappingObject { get; set; } = new List<EmailMappingModel>();
        public List<EmailRecipient> EmailRecipients { get; set; } = new List<EmailRecipient>();


    }
}
