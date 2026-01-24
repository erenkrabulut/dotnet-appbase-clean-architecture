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
    public class UserRoleRepository
        : EFRepository<UserRole, Guid, BaseDbContext>, IUserRoleRepository
    {
        public UserRoleRepository(BaseDbContext context) : base(context) { }
    }
}
