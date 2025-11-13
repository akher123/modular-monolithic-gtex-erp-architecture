using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class CompanyOperatingState
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int OperatingStateId { get; set; }
        public bool IsDefault { get; set; }

        public Company Company { get; set; }
        public State OperatingState { get; set; }
    }
}
