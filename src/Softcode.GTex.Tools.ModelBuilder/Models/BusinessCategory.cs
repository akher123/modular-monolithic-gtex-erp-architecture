using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessCategory
    {
        public int Id { get; set; }
        public int BusinessCategoryTypeId { get; set; }
        public int? BusinessCategoryMapTypeOptionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }

        public BusinessCategoryMapTypeOption BusinessCategoryMapTypeOption { get; set; }
        public BusinessCategoryType BusinessCategoryType { get; set; }
    }
}
