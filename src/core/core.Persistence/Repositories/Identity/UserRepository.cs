using core.Application.Abstractions.Repositories.Identity;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace core.Persistence.Repositories.Identity
{
    public class UserRepository
        : EFRepository<User, Guid, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _set.FirstOrDefaultAsync(
                u => u.Email == email,
                cancellationToken
            );
        }
    }
}
