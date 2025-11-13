using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.Data.Models
{
    public class ApplicationRole : IdentityRole, ITrackable
    {
        public ApplicationRole()
        {
            RoleRights = new HashSet<ApplicationRoleRight>();
            InverseParentRole = new HashSet<ApplicationRole>();
        }

        [MaxLength(400)]
        public string Description { get; set; }

        public string ParentRoleId { get; set; }

       public int? BusinessProfileId { get; set; }        

        public bool IsActive { get; set; } = true;

        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public BusinessProfile BusinessProfile { get; set; }

        public DateTime CreatedDateTime { get ; set; }
        public int CreatedByContactId { get; set; }
        public Contact CreatedByContact { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ApplicationRole ParentRole { get; set; }

        public ICollection<ApplicationRole> InverseParentRole { get; set; }
        public ICollection<ApplicationRoleRight> RoleRights { get; set; }
    }
}
