using core.Domain.Entities.Identity;
using core.Domain.Security;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IUserLoginService
    {
        Task<UserLogin?> TryGetByIdAsync(Guid id, CancellationToken ct = default);
        Task<UserLogin> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<List<UserLogin>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);

        Task<UserLogin?> TryGetByProviderAsync(Guid userId, string providerKey, CancellationToken ct = default);
        Task<UserLogin> GetByProviderAsync(Guid userId, string providerKey, CancellationToken ct = default);

        Task<UserLogin?> TryGetByUserAndProviderAsync(Guid userId, AuthenticationProvider provider, CancellationToken ct = default);

        Task EnsureProviderKeyUniqueAsync(AuthenticationProvider provider, string providerKey, Guid currentUserId, CancellationToken ct = default);

        Task LinkAsync(Guid userId, AuthenticationProvider provider, string providerKey, string? providerValue, CancellationToken ct = default);

        Task UnlinkAsync(Guid userId, AuthenticationProvider provider, CancellationToken ct = default);

        Task<UserLogin> CreateAsync(UserLogin userLogin, CancellationToken ct = default);

        Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default);
    }
}
