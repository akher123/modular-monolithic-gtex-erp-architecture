using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ContactBusinessProfile
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public int? EntityTypeId { get; set; }
        public int? BusinessProfileId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact Contact { get; set; }
        public EntityType EntityType { get; set; }
    }
}
