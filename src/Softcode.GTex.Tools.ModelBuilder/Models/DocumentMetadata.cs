using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class DocumentMetadata
    {
        public DocumentMetadata()
        {
            DocumentFileStore = new HashSet<DocumentFileStore>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        public string Description { get; set; }
        public int? DocumentTypeId { get; set; }
        public byte[] TimeStamp { get; set; }
        public bool IsInternal { get; set; }
        public bool IsArchived { get; set; }
        public int BusinessProfileId { get; set; }
        public string Keywords { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public CustomCategory DocumentType { get; set; }
        public EntityType EntityType { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ICollection<DocumentFileStore> DocumentFileStore { get; set; }
    }
}
