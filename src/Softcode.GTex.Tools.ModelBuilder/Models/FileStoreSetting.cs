using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class FileStoreSetting
    {
        public int Id { get; set; }
        public int HostType { get; set; }
        public string HostName { get; set; }
        public string HostIp { get; set; }
        public string HostDomain { get; set; }
        public string HostUsername { get; set; }
        public string HostPassword { get; set; }
        public string ShareName { get; set; }
        public string ShareDirectoryPath { get; set; }
        public int Status { get; set; }
        public byte[] TimeStamp { get; set; }
        public int BusinessProfileId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
    }
}
