using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.Api.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string ReturnUrl { get; set; }
    }
}
