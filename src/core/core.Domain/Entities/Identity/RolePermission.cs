using core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Entities.Identity
{
    public class RolePermission : Entity<Guid>
    {
        public Guid RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;

        protected RolePermission() { }

        public RolePermission(Guid roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}
