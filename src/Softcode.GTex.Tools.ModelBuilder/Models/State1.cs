using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class State1
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Data { get; set; }

        public Job Job { get; set; }
    }
}
