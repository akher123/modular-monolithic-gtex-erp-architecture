using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Job
    {
        public Job()
        {
            JobParameter = new HashSet<JobParameter>();
            State1 = new HashSet<State1>();
        }

        public int Id { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string InvocationData { get; set; }
        public string Arguments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }

        public ICollection<JobParameter> JobParameter { get; set; }
        public ICollection<State1> State1 { get; set; }
    }
}
