using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService.Models
{
    public class EntityContactModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int EntityType { get; set; }
        public int EntityId { get; set; }
    }
}
