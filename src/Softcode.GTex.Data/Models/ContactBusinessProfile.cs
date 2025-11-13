using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Models
{
    public partial class ContactBusinessProfile
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int EntityTypeId { get; set; }
        public int BusinessProfileId { get; set; }
        public Contact Contact { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
        public EntityType EntityType { get; set; }
    }
}
