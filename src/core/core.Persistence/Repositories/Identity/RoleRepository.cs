using core.Application.Abstractions.Repositories.Identity;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace core.Persistence.Repositories.Identity
{
    public class RoleRepository
        : EFRepository<Role, Guid, BaseDbContext>, IRoleRepository
    {
        public RoleRepository(BaseDbContext context) : base(context) { }

        public Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return _set.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
    }
}
