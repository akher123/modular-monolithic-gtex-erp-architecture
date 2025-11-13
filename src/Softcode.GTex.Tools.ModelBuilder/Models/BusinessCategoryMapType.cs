using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessCategoryMapType
    {
        public BusinessCategoryMapType()
        {
            BusinessCategoryMapTypeOption = new HashSet<BusinessCategoryMapTypeOption>();
            BusinessCategoryType = new HashSet<BusinessCategoryType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<BusinessCategoryMapTypeOption> BusinessCategoryMapTypeOption { get; set; }
        public ICollection<BusinessCategoryType> BusinessCategoryType { get; set; }
    }
}
