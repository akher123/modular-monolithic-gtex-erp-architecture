using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessCategoryType
    {
        public BusinessCategoryType()
        {
            BusinessCategory = new HashSet<BusinessCategory>();
        }

        public int Id { get; set; }
        public int? RowNo { get; set; }
        public string RoutingKey { get; set; }
        public string Name { get; set; }
        public int? BusinessCategoryMapTypeId { get; set; }

        public BusinessCategoryMapType BusinessCategoryMapType { get; set; }
        public ICollection<BusinessCategory> BusinessCategory { get; set; }
    }
}
