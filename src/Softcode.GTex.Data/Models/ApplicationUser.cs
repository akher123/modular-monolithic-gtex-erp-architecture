using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softcode.GTex.Data.Models
{
    public class ApplicationUser : IdentityUser,ITrackable
    {
        public ApplicationUser()
        {
            AccessLogs = new HashSet<AccessLog>();
            UserBusinessProfiles = new HashSet<UserBusinessProfile>();
        }

        [Required]
        public int IdentityType { get; set; }

        [StringLength(400)]
        public string SID { get; set; }

        public bool IsActive { get; set; } = true;

        public int ContactId { get; set; }

        public DateTime? UnlockDateTime { get; set; }

        public DateTime? NextPasswordChangeDate { get; set; }

        public bool IsPendingAuthentication { get; set; } = false;

        public int? SecurityProfileId { get; set; }

        public bool EnableWCAG { get; set; }

        public bool AdminLocked { get; set; }

        

        public bool RequireChangePassword { get; set; } = false;

        public int ContactTypeId { get; set; }

        [Timestamp]
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }
        public string ShortUserName { get; set; }
        public string DomainName { get; set; }
        public SecurityProfile SecurityProfile { get; set; }

        public Contact Contact { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
    
        public ICollection<AccessLog> AccessLogs { get; set; }
        public ICollection<UserBusinessProfile> UserBusinessProfiles { get; set; }
    }
}
