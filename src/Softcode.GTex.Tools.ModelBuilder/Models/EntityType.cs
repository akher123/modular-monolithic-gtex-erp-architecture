using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
            Communication = new HashSet<Communication>();
            ContactBusinessProfile = new HashSet<ContactBusinessProfile>();
            DocumentMetadata = new HashSet<DocumentMetadata>();
            EntityContact = new HashSet<EntityContact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInternal { get; set; }

        public ICollection<Communication> Communication { get; set; }
        public ICollection<ContactBusinessProfile> ContactBusinessProfile { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadata { get; set; }
        public ICollection<EntityContact> EntityContact { get; set; }
    }
}
