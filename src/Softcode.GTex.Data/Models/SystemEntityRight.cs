using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class SystemEntityRight
    {
        public SystemEntityRight()
        {
            ApplicationMenus = new HashSet<ApplicationMenu>();
            ApplicationRoleRights = new HashSet<ApplicationRoleRight>();
            CustomCategoryTypes = new HashSet<CustomCategoryType>();
            SystemEntityRightDependencyDependentRights = new HashSet<SystemEntityRightDependency>();
            SystemEntityRightDependencyRights = new HashSet<SystemEntityRightDependency>();
        }

        public int Id { get; set; }
        public int EntityId { get; set; }
        public string RightKey { get; set; }
        public string Name { get; set; }
        public int RightType { get; set; }
        public string Description { get; set; }
        public bool IsB2bItem { get; set; }
        public int SortOrder { get; set; }
        public bool? IsActive { get; set; }
        public byte[] TimeStamp { get; set; }

        public SystemEntity SystemEntity { get; set; }
        public ICollection<ApplicationMenu> ApplicationMenus { get; set; }
        public ICollection<ApplicationRoleRight> ApplicationRoleRights { get; set; }
        public ICollection<CustomCategoryType> CustomCategoryTypes { get; set; }
        public ICollection<SystemEntityRightDependency> SystemEntityRightDependencyDependentRights { get; set; }
        public ICollection<SystemEntityRightDependency> SystemEntityRightDependencyRights { get; set; }
    }
}
