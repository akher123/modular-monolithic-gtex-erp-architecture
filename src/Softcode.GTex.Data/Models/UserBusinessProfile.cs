using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Models
{
    public partial class UserBusinessProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BusinessProfileId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public ApplicationUser User { get; set; }
    }
}
