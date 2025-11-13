namespace Softcode.GTex
{
    public interface ILoggedInUser
    {
        string UserId { get; set; }
        int ContectId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string FullName { get; set; }
        string[] RoleIds { get; set; }
        string[] DefaultBusinessProfileRoleIds { get; set; }
        string[] DefaultBusinessProfileRoleHierarchyIds { get; set; }
        string[] RoleNames { get; set; }
        int DefaultBusinessProfileId { get; set; }
        int[] UserBusinessProfileIds { get; set; }
        int[] ContactBusinessProfileIds { get; set; }
        string EmailAddress { get; set; }
        string PhoneNumber { get; set; }
        string MobileNumber { get; set; }
        string Gender { get; set; }
        string ImageSource { get; set; }
        bool IsDefaultBusinessProfile { get; set; }
        bool IsSuperAdmin { get; set; }
    }
}
