using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService.Models
{
    public class TreeModel
    {
        public int Id { get; set; }
        public string SId { get; set; }
        public int ParentId { get; set; }
        public string SParentId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public bool IsExpanded { get; set; } = true;
        public bool Disabled { get; set; } = false;
        public bool Visible { get; set; }= true;
        public List<TreeModel> Items { get; set; }
    }
}
