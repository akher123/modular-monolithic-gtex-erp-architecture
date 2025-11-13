using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class SystemModule
    {
        public SystemModule()
        {
            SystemEntity = new HashSet<SystemEntity>();
        }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public byte[] TimeStamp { get; set; }

        public ICollection<SystemEntity> SystemEntity { get; set; }
    }
}
