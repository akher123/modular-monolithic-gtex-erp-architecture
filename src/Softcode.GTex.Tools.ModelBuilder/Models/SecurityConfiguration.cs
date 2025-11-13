using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class SecurityConfiguration
    {
        public int Id { get; set; }
        public int? BusinessProfileId { get; set; }
        public int MaximumSesssionSpaceInKb { get; set; }
        public int? MaximumImageUploadSizeInKb { get; set; }
        public int DefaultUserAuthType { get; set; }
        public int B2busernameType { get; set; }
        public int EmployeeUsernameType { get; set; }
        public int OtherUsernameType { get; set; }
        public bool EnableDataAuditLog { get; set; }
        public bool EnableErrorLog { get; set; }
        public bool? EnableSessionLog { get; set; }
        public bool EnableAuthenticationLog { get; set; }
        public bool EnableAccessibilityCompliant { get; set; }
        public bool EnableAutoLogin { get; set; }
        public bool? EnableRetrievePassword { get; set; }
        public bool EnableCaptcha { get; set; }
        public bool EnablePasswordResetRestriction { get; set; }
        public int NumberOfAttemptsBeforeCaptcha { get; set; }
        public bool EnableSso { get; set; }
        public bool DeletePhysicalDocument { get; set; }
        public bool MustAcceptTerms { get; set; }
        public string ApplicationTitle { get; set; }
        public string AppHelpContentUrl { get; set; }
        public byte[] OtherSettings { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
    }
}
