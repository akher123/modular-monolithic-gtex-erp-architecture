using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging.Models
{
    [Serializable]
    public class EmailMappingModel
    {
        public string key { get; set; }
        public string Value { get; set; }
    }
}
