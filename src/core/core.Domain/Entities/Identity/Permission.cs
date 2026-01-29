using core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Entities.Identity
{
    public class Permission : Entity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = null!;


        public Permission(string name) 
        {
            Name = name;
            RolePermissions = new List<RolePermission>();
        }
    }
}
