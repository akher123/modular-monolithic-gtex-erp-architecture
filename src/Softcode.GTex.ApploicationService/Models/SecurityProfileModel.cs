using Softcode.GTex.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Softcode.GTex.ApploicationService.Models
{
    /// <summary>
    /// SecurityProfileModel Service Model
    /// </summary>
    public class SecurityProfileModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Profile Name is required")]
        [MaxLength(200, ErrorMessage = "Maximum length of profile name is 200 characters.")]
        public string ProfileName { get; set; }
        public int? BusinessProfileId { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum length of description is 200 characters.")]
        public string Descriptions { get; set; }
        public bool IsDefault { get; set; }
        public int MinPasswordLength { get; set; } = 6;
        public int MaxPasswordLength { get; set; } = 32;
        public int MinLowerCaseCharacter { get; set; }
        public int MinUpperCaseCharacter { get; set; }
        public int MinDigit { get; set; }
        public int MinSpecialCharacter { get; set; }
        public int PasswordCombinationTypeId { get; set; } = PasswordCombinationType.Any;
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
    }
}
