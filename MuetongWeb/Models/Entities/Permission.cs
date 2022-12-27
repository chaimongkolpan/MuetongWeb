using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
