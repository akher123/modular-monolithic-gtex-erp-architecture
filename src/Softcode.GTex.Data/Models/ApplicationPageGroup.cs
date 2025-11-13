using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationPageGroup
    {
        public ApplicationPageGroup()
        {
            ApplicationPageDetailFields = new HashSet<ApplicationPageDetailField>();
        }

        public int Id { get; set; }
        public int PageId { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public int ColumnsCount { get; set; }
        public int SortOrder { get; set; }        
        public ApplicationPage Page { get; set; }
        public ICollection<ApplicationPageDetailField> ApplicationPageDetailFields { get; set; }
    }
}
