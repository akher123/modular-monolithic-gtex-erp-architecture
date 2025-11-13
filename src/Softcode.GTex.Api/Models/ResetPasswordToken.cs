namespace Softcode.GTex.Api.Models
{
    public class ResetPasswordToken
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string ClientId { get; set; }
    }
}
