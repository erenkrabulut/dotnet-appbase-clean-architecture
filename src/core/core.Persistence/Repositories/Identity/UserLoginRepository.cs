using core.Application.Abstractions.Repositories.Identity;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace core.Persistence.Repositories.Identity
{
    public class UserLoginRepository
        : EFRepository<UserLogin, Guid, BaseDbContext>, IUserLoginRepository
    {
        public UserLoginRepository(BaseDbContext context) : base(context) { }

        public Task<UserLogin?> GetByProviderAsync(
            Guid userId,
            string providerKey,
            CancellationToken cancellationToken = default)
        {
            return _set.FirstOrDefaultAsync(
                x => x.UserId == userId && x.ProviderKey == providerKey,
                cancellationToken
            );
        }
    }
}
