using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AccessLog = new HashSet<AccessLog>();
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int IdentityType { get; set; }
        public string Sid { get; set; }
        public bool IsActive { get; set; }
        public int ContactId { get; set; }
        public DateTime? UnlockDateTime { get; set; }
        public DateTime? NextPasswordChangeDate { get; set; }
        public bool IsPendingAuthentication { get; set; }
        public int? SecurityProfileId { get; set; }
        public bool EnableWcag { get; set; }
        public bool IsAutoInactivated { get; set; }
        public bool RequireChangePassword { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public Contact Contact { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public SecurityProfile SecurityProfile { get; set; }
        public ICollection<AccessLog> AccessLog { get; set; }
        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
    }
}
