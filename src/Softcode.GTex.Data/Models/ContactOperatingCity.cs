using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ContactOperatingCity
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string OperatingCity { get; set; }
        public bool IsDefault { get; set; }

        public Contact Contact { get; set; }
    }
}
