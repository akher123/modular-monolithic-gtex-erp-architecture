using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class SystemEntityRight
    {
        public SystemEntityRight()
        {
            ApplicationMenu = new HashSet<ApplicationMenu>();
            ApplicationRoleRight = new HashSet<ApplicationRoleRight>();
            CustomCategoryType = new HashSet<CustomCategoryType>();
            SystemEntityRightDependencyDependentRight = new HashSet<SystemEntityRightDependency>();
            SystemEntityRightDependencyRight = new HashSet<SystemEntityRightDependency>();
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

        public SystemEntity Entity { get; set; }
        public ICollection<ApplicationMenu> ApplicationMenu { get; set; }
        public ICollection<ApplicationRoleRight> ApplicationRoleRight { get; set; }
        public ICollection<CustomCategoryType> CustomCategoryType { get; set; }
        public ICollection<SystemEntityRightDependency> SystemEntityRightDependencyDependentRight { get; set; }
        public ICollection<SystemEntityRightDependency> SystemEntityRightDependencyRight { get; set; }
    }
}
