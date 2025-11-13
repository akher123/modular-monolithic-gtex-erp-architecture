using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.ApplicantionService.Messaging.Models
{
    public class EmailServerModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Maximum length of name is 100 characters.")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Protocol is required")]
        public int ProtocolId { get; set; }
        [Required(ErrorMessage = "Server type is required")]
        public int ServerType { get; set; }
        [Required(ErrorMessage = "SMTP server is required")]
        [MaxLength(100, ErrorMessage = "Maximum length of SMTP server is 100 characters.")]
        public string OutgoingServer { get; set; }
        [Required(ErrorMessage = "SMTP port is required")]
        [Range(0, 65535, ErrorMessage = "SMTP port must be between 0 and 65535")]
        public int OutgoingPort { get; set; } = 25;
        [MaxLength(100, ErrorMessage = "Maximum length of sender name is 100 characters.")]        
        public string SenderName { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of sender email address is 100 characters.")]
        public string SenderId { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of user name is 100 characters.")]
        public string UserName { get; set; }        
        public string Password { get; set; }
        [Required(ErrorMessage = "Authentication type is required")]
        public int AuthenticationTypeId { get; set; }
        [Required(ErrorMessage = "Sender option is required")]
        public int SenderOptionId { get; set; }        
        public bool UseSslforOutgoing { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of reply to email address is 100 characters.")]
        public string ReplyToEmailAddress { get; set; }
        [MaxLength(100, ErrorMessage = "Maximum length of send copy to email address is 100 characters.")]
        public string CopyToEmailAddress { get; set; }
        public bool IsDefault { get; set; }
        [Required(ErrorMessage = "Business profile is required")]
        public int BusinessProfileId { get; set; }
    }
}
