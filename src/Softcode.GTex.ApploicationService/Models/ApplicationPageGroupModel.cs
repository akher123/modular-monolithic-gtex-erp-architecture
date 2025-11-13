using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService.Models
{
    public class ApplicationPageGroupModel
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public int ColumnsCount { get; set; }
        public int SortOrder { get; set; }
    }
}
