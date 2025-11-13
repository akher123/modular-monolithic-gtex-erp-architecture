using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class CustomCategoryType
    {
        public CustomCategoryType()
        {
            CustomCategories = new HashSet<CustomCategory>();
        }

        public int Id { get; set; }
        public string RoutingKey { get; set; }
        public string ModuleName { get; set; }
        public string Name { get; set; }
        public string HelpText { get; set; }
        public int RightId { get; set; }
        public int RowNo { get; set; }
        public int? CustomCategoryMapTypeId { get; set; }
        public string ImageSource { get; set; }
        public bool IsMapTypeRequired { get; set; }
        public bool IsMapTypeMappingUnique { get; set; }

        public CustomCategoryMapType CustomCategoryMapType { get; set; }
        public SystemEntityRight Right { get; set; }
        public ICollection<CustomCategory> CustomCategories { get; set; }
    }
}
