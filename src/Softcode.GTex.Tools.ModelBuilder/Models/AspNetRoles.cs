using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            ApplicationRoleRight = new HashSet<ApplicationRoleRight>();
            AspNetRoleClaims = new HashSet<AspNetRoleClaims>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            InverseParentRole = new HashSet<AspNetRoles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Description { get; set; }
        public string ParentRoleId { get; set; }
        public int? BusinessProfileId { get; set; }
        public bool IsActive { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public AspNetRoles ParentRole { get; set; }
        public ICollection<ApplicationRoleRight> ApplicationRoleRight { get; set; }
        public ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<AspNetRoles> InverseParentRole { get; set; }
    }
}
