using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class CustomCategoryMapType
    {
        public CustomCategoryMapType()
        {
            CustomCategoryMapTypeOptions = new HashSet<CustomCategoryMapTypeOption>();
            CustomCategoryTypes = new HashSet<CustomCategoryType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CustomCategoryMapTypeOption> CustomCategoryMapTypeOptions { get; set; }
        public ICollection<CustomCategoryType> CustomCategoryTypes { get; set; }
    }
}
