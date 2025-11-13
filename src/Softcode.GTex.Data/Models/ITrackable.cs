using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Models
{
    public interface ITrackable
    {
        DateTime CreatedDateTime { get; set; }
        int CreatedByContactId { get; set; }
        Contact CreatedByContact { get; set; }
        DateTime? LastUpdatedDateTime { get; set; }
        int? LastUpdatedByContactId { get; set; }
        Contact LastUpdatedByContact { get; set; }
    }
}
