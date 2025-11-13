using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging.Models
{
    public class EmailTemplateModel : ITrackable
    {
        public int Id { get; set; }
        public int BusinessMapTypeId { get; set; }
        public string Name { get; set; }
        public int BusinessProfileId { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public int EmailServerId { get; set; }
        public bool UseLoggedInUserEmail { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public Contact CreatedByContact { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }
        public Contact LastUpdatedByContact { get; set; }
    }
}
