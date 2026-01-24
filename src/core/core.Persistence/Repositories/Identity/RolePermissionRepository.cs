using core.Application.Abstractions.Repositories.Identity;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Repositories.Identity
{
    internal class RolePermissionRepository
        : EFRepository<RolePermission, Guid, BaseDbContext>, IRolePermissionRepository
    {
        public RolePermissionRepository(BaseDbContext context) : base(context) { }
    }
}
