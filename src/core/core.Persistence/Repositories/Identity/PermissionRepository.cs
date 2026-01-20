using core.Application.Repositories.Identity;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace core.Persistence.Repositories.Identity
{
    public class PermissionRepository
        : EFRepository<Permission, int, BaseDbContext>, IPermissionRepository
    {
        public PermissionRepository(BaseDbContext context) : base(context) { }

        public Task<Permission?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return _set.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }
    }
}
