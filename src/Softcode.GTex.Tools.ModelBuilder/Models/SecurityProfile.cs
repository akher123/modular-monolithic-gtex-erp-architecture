using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class SecurityProfile
    {
        public SecurityProfile()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int Id { get; set; }
        public string ProfileName { get; set; }
        public int? BusinessProfileId { get; set; }
        public string Descriptions { get; set; }
        public bool IsDefault { get; set; }
        public int MinPasswordLength { get; set; }
        public int MaxPasswordLength { get; set; }
        public bool RequireLowerCaseCharacter { get; set; }
        public int MinLowerCaseCharacter { get; set; }
        public bool RequireUpperCaseCharacter { get; set; }
        public int MinUpperCaseCharacter { get; set; }
        public bool RequireDigit { get; set; }
        public int MinDigit { get; set; }
        public bool RequireSpecialCharacter { get; set; }
        public int MinSpecialCharacter { get; set; }
        public bool DisallowUserNameInPassword { get; set; }
        public bool DisallowPartsOfNameInPassword { get; set; }
        public int NumberOfRememberedPassword { get; set; }
        public int MaximumPasswordAge { get; set; }
        public int MinimumPasswordAge { get; set; }
        public bool EnableAccountLockout { get; set; }
        public int NumberOfAttemptsBeforeLockout { get; set; }
        public int LockoutDuration { get; set; }
        public int ResetLockoutCounterAfter { get; set; }
        public int MakeInactiveIfNotUsed { get; set; }
        public int DefaultSessionTimeout { get; set; }
        public bool AllowUserToChangeSessionTime { get; set; }
        public int MaximumConcurrentSession { get; set; }
        public bool IsActive { get; set; }
        public byte[] TimeStamp { get; set; }
        public int? RoleMapType { get; set; }
        public int ResetUrlexpiryInHours { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
