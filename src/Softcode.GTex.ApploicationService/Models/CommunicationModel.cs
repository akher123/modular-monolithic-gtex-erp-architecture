using System;
using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.ApploicationService.Models
{
    public class CommunicationModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Business profile is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Not a valid business profile")]
        public int BusinessProfileId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="Subject is required")]
        [MaxLength(400, ErrorMessage = "Maximum length of subject is 400 characters.")]
        public string Subject { get; set; }
        public string Notes { get; set; }
        public DateTimeOffset? CommunicationDateTime { get; set; }
        public bool IsReminderEnable { get; set; }
        public DateTimeOffset? ReminderDateTime { get; set; }
        public int? MethodTypeId { get; set; }
        public int? StatusId { get; set; }
        public bool IsFollowupEnable { get; set; }
        public int? FollowupByContactId { get; set; }
        public DateTimeOffset? FollowupDate { get; set; }
    }
}
