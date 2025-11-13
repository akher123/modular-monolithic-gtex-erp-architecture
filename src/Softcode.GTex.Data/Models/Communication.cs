using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class Communication: ITrackable
    {
        public Communication()
        {
            CommunicationFileStores = new HashSet<CommunicationFileStore>();
        }

        public int Id { get; set; }
        public int BusinessProfileId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public string Subject { get; set; }
        public string Notes { get; set; }
        public DateTime CommunicationDateTime { get; set; }
        public bool IsReminderEnable { get; set; }
        public DateTime? ReminderDateTime { get; set; }
        public int? MethodTypeId { get; set; }
        public int? StatusId { get; set; }
        public bool IsFollowupEnable { get; set; }
        public int? FollowupByContactId { get; set; }
        public DateTime? FollowupDate { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
        public Contact CreatedByContact { get; set; }
        public EntityType EntityType { get; set; }
        public Contact FollowupByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public CustomCategory MethodType { get; set; }
        public CustomCategory Status { get; set; }

        public ICollection<CommunicationFileStore> CommunicationFileStores { get; set; }
    }
}
