using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class CustomCategoryMapType
    {
        public CustomCategoryMapType()
        {
            CustomCategoryMapTypeOption = new HashSet<CustomCategoryMapTypeOption>();
            CustomCategoryType = new HashSet<CustomCategoryType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CustomCategoryMapTypeOption> CustomCategoryMapTypeOption { get; set; }
        public ICollection<CustomCategoryType> CustomCategoryType { get; set; }
    }
}
