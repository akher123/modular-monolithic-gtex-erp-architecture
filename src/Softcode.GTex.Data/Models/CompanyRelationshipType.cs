using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class CompanyRelationshipType
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int RelationshipTypeId { get; set; }

        public Company Company { get; set; }
    }
}
