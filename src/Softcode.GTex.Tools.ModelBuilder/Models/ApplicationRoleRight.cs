using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationRoleRight
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int RightId { get; set; }

        public SystemEntityRight Right { get; set; }
        public AspNetRoles Role { get; set; }
    }
}
