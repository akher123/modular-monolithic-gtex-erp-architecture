using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Models
{
    public partial class CompanyDepartment : Department
    {
         
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        [NotMapped]
        public override int EntityId { get { return CompanyId; } set { CompanyId = value; } }


    }
}
