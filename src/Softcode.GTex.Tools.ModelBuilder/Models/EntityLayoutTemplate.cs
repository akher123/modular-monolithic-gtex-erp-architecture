using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EntityLayoutTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EntityType { get; set; }
        public int? EntityId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public byte[] TimeStamp { get; set; }
        public string Guid { get; set; }
        public int BusinessProfileId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
    }
}
