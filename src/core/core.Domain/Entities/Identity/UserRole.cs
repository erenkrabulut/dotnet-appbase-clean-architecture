using core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Entities.Identity
{
    public class UserRole : Entity<Guid>
    {
        public Guid UserId { get;  set; }
        public Guid RoleId { get;  set; }

        public virtual User User { get; set; } = null!;
        public virtual Role Role { get;  set; } = null!;


        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
