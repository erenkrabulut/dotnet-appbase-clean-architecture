using core.Application.Abstractions.Repositories.Identity;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;

namespace core.Persistence.Repositories.Identity
{
    public class UserRoleRepository
        : EFRepository<UserRole, Guid, BaseDbContext>, IUserRoleRepository
    {
        public UserRoleRepository(BaseDbContext context) : base(context) { }
    }
}
