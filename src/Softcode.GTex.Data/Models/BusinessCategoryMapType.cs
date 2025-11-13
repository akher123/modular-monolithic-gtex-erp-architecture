using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessCategoryMapType
    {
        public BusinessCategoryMapType()
        {
        
        }

        public int Id { get; set; }
        public int BusinessCategoryId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public BusinessCategory BusinessCategory { get; set; }
        
    }
}
