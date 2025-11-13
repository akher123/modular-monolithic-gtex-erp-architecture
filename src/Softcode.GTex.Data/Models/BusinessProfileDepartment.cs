using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Models
{
    public partial class BusinessProfileDepartment : Department
    {

        public int BusinessProfileId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }

        //[NotMapped]
        //public override int EntityId { get { return BusinessProfileId; } set { BusinessProfileId = value; } }
    }
}
