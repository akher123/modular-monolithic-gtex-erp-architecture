using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Server
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int Protocol { get; set; }
        public int ServerType { get; set; }
        public string OutgoingServer { get; set; }
        public int OutgoingPort { get; set; }
        public string IncomingServer { get; set; }
        public int IncomingPort { get; set; }
        public string SenderName { get; set; }
        public string SenderId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserAuthenticationType { get; set; }
        public int SenderOption { get; set; }
        public bool UseSslforIncoming { get; set; }
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

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
    }
}
