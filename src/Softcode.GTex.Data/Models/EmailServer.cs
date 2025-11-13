using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EmailServer : ITrackable
    {
        public EmailServer()
        {
            EmailTemplates = new HashSet<EmailTemplate>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int ProtocolId { get; set; }
        public int ServerType { get; set; }
        public string OutgoingServer { get; set; }
        public int OutgoingPort { get; set; }   
        public string SenderName { get; set; }
        public string SenderId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AuthenticationTypeId { get; set; }
        public int SenderOptionId { get; set; }        
        public bool UseSslforOutgoing { get; set; }
        public string ReplyToEmailAddress { get; set; }
        public string CopyToEmailAddress { get; set; }
        public bool IsDefault { get; set; }
        public byte[] TimeStamp { get; set; }
        public int BusinessProfileId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessCategory AuthenticationType { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public BusinessCategory Protocol { get; set; }
        public BusinessCategory SenderOption { get; set; }
        public ICollection<EmailTemplate> EmailTemplates { get; set; }

    }
}
