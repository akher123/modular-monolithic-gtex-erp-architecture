namespace Softcode.GTex.ApploicationService.Models
{
    public class LoggedInUser : ILoggedInUser
    {
        public string UserId { get; set; }
        public int ContectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string[] RoleIds { get; set; }
        public string[] DefaultBusinessProfileRoleIds { get; set; }
        public string[] DefaultBusinessProfileRoleHierarchyIds { get; set; }
        public string[] RoleNames { get; set; }
        public int DefaultBusinessProfileId { get; set; }
        public int[] UserBusinessProfileIds { get; set; }
        public int[] ContactBusinessProfileIds { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string ImageSource { get; set; }
        public bool IsDefaultBusinessProfile { get; set; }
        public bool IsSuperAdmin { get; set; }
        
    }
}
