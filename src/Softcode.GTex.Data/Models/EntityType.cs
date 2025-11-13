using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
            Communications = new HashSet<Communication>();
            DocumentMetadatas = new HashSet<DocumentMetadata>();
            EntityContacts = new HashSet<EntityContact>();
            ContactBusinessProfiles = new HashSet<ContactBusinessProfile>();
            RecordInfos = new HashSet<RecordInfo>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AddressRelationClass { get; set; }
        public string DocumentRelationClass { get; set; }
        public bool IsInternal { get; set; }

        

        public ICollection<Communication> Communications { get; set; }
        public ICollection<DocumentMetadata> DocumentMetadatas { get; set; }
        public ICollection<EntityContact> EntityContacts { get; set; }
        public ICollection<ContactBusinessProfile> ContactBusinessProfiles { get; set; }
        public ICollection<RecordInfo> RecordInfos { get; set; }
    }
}
