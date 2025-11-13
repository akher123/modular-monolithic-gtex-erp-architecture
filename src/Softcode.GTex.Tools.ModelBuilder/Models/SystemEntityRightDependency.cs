using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class SystemEntityRightDependency
    {
        public int Id { get; set; }
        public int RightId { get; set; }
        public int DependentRightId { get; set; }

        public SystemEntityRight DependentRight { get; set; }
        public SystemEntityRight Right { get; set; }
    }
}
