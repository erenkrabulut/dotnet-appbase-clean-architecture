using core.Domain.Entities.Identity;

namespace core.Application.Abstractions.Repositories.Identity
{
    public interface IUserLoginRepository : IRepository<UserLogin, Guid>
    {
        Task<UserLogin?> GetByProviderAsync(
            Guid userId,
            string providerKey,
            CancellationToken cancellationToken = default
        );
    }
}
