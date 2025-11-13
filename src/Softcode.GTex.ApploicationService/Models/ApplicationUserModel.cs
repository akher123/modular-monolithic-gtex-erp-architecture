using System;
using System.Collections.Generic;

namespace Softcode.GTex.ApploicationService.Models
{
    public class ApplicationUserModel
    {
        public ApplicationUserModel()
        {
            this.Contact = new ContactModel();
            this.UserRoles = new List<string>();
            this.BusinessProfileIds = new List<int>();
        }


        #region User Related

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }

        public bool IsActive { get; set; } 

        public int? ContactId { get; set; }

        public DateTime? UnlockDateTime { get; set; }

        public DateTime? NextPasswordChangeDate { get; set; }

        public bool IsPendingAuthentication { get; set; }

        public int? SecurityProfileId { get; set; }

        public bool EnableWCAG { get; set; }

        public bool AdminLocked { get; set; } 

        public bool EmailConfirmed { get; set; } 

        public bool PhoneNumberConfirmed { get; set; } 

        public bool TwoFactorEnabled { get; set; } 

        public int AccessFailedCount { get; set; }

        public int IdentityType { get; set; }

        public bool RequireChangePassword { get; set; }

        public int ContactTypeId { get; set; }
        public string ShortUserName { get; set; }
        public string DomainName { get; set; }
        public int BusinessProfileId { get; set; }
        public List<int> BusinessProfileIds { get; set; }
        public ContactModel Contact { get; set; }

        public List<string> UserRoles { get; set; } 
        public bool IsSuperAdmin { get; set; } = false;

        public bool SystemLocked { get; set; } = false;

        #endregion
    }
}
