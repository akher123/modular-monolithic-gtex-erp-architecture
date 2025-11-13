namespace Softcode.GTex.ApploicationService.Models
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual string NormalizedName { get; set; }
        public string Description { get; set; }
        public string ParentRoleId { get; set; }
        public string CopyRoleId { get; set; }
        public int? BusinessProfileId { get; set; }
        public bool IsActive { get; set; } = true;
        public int[] RoleRights { get; set; }
    }
}
