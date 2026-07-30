using core.Domain.Entities.Identity;

namespace core.Application.Abstractions.Repositories.Identity
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken, Guid>
    {
        Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default);

        Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default);

        Task RevokeAllByUserIdAsync(Guid userId, string ipAddress, string reason, CancellationToken ct = default);
    }
}
