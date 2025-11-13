using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessCategoryType
    {
        public BusinessCategoryType()
        {
            BusinessCategories = new HashSet<BusinessCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<BusinessCategory> BusinessCategories { get; set; }
    }
}
