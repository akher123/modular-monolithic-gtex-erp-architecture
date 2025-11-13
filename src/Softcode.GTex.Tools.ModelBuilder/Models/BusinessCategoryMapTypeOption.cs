using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessCategoryMapTypeOption
    {
        public BusinessCategoryMapTypeOption()
        {
            BusinessCategory = new HashSet<BusinessCategory>();
        }

        public int Id { get; set; }
        public int? BusinessCategoryMapTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public BusinessCategoryMapType BusinessCategoryMapType { get; set; }
        public ICollection<BusinessCategory> BusinessCategory { get; set; }
    }
}
