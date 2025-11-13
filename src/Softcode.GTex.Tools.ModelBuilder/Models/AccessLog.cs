using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class AccessLog
    {
        public int Id { get; set; }
        public int? RecordRowId { get; set; }
        public int LogType { get; set; }
        public int SessionId { get; set; }
        public string UserId { get; set; }
        public byte[] Token { get; set; }
        public DateTime LogDateTime { get; set; }
        public string EntityName { get; set; }
        public string LogDescription { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public AspNetUsers User { get; set; }
    }
}
