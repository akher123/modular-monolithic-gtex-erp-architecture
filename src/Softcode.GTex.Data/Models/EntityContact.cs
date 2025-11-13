using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EntityContact
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }

        public Contact Contact { get; set; }

        public EntityType EntityType { get; set; }

        public Company Company { get; set; }
    }
}
