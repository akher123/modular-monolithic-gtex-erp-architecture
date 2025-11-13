using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public int AppServiceId { get; set; }
        public DateTime ErrorDateTime { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public string UserLocation { get; set; }
        public string UserIp { get; set; }
        public string MachineUserName { get; set; }
        public string DomainName { get; set; }
        public string UserMachineName { get; set; }
        public string Osversion { get; set; }
        public int ApplicationType { get; set; }
        public string Resolution { get; set; }
        public string ApplicationVersion { get; set; }
        public bool IsClosed { get; set; }
        public bool IsReported { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int BusinessProfileId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
    }
}
