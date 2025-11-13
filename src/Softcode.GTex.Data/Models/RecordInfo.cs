using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Models
{
    public class RecordInfo
    {
        public RecordInfo()
        {
            BusinessProfiles = new HashSet<BusinessProfile>();
            Contacts = new HashSet<Contact>();
            Companies = new HashSet<Company>();
        }
        public Guid Id { get; set; }
        public int EntityTypeId { get; set; }
        //public int EntityId { get; set; }
        public EntityType EntityType { get; set; }
        public ICollection<BusinessProfile> BusinessProfiles { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
