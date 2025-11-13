using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService.Models
{
    public class SecurityConfigurationModel
    {
        public int Id { get; set; }
        public int MaximumSesssionSpaceInKB { get; set; }
        public Nullable<int> MaximumImageUploadSizeInKB { get; set; }
        public int DefaultUserAuthType { get; set; }
        public int B2BUsernameType { get; set; }
        public int EmployeeUsernameType { get; set; }
        public int OtherUsernameType { get; set; }
        public bool EnableDataAuditLog { get; set; }
        public bool EnableErrorLog { get; set; }
        public bool EnableSessionLog { get; set; }
        public bool EnableAuthenticationLog { get; set; }
        public bool EnableAccessibilityCompliant { get; set; }
        public bool EnableAutoLogin { get; set; }
        public bool EnableRetrievePassword { get; set; }
        public bool EnableCaptcha { get; set; }
        public bool EnablePasswordResetRestriction { get; set; }
        public int NumberOfAttemptsBeforeCaptcha { get; set; }
        public bool EnableSSO { get; set; }
        public bool DeletePhysicalDocument { get; set; }
        public bool MustAcceptTerms { get; set; }
        public string ApplicationTitle { get; set; }
        public string AppHelpContentURL { get; set; }
        public byte[] OtherSettings { get; set; }
    }
}
