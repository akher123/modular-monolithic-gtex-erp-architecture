using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EmailRecipient
    {
        public int Id { get; set; }
        public int? EmailJobQueueId { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string RecipientType { get; set; }
        public EmailJobQueue EmailJobQueue { get; set; }
      
    }
}
