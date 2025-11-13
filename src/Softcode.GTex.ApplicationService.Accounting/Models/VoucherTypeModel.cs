using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Accounting.Models
{
    public class VoucherTypeModel
    {
        public int VoucherTypeId { get; set; }
        public string VoucherTypeRefId { get; set; }
        public string TypeName { get; set; }
        public string Remarks { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
