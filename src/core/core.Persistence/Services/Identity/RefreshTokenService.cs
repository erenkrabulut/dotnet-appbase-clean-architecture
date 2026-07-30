using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Domain.Entities.Identity;

namespace core.Persistence.Services.Identity
{
    public sealed class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public Task<RefreshToken?> TryGetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _refreshTokenRepository.GetByIdAsync(id, ct);
        }

        public async Task<RefreshToken> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            RefreshToken? token = await TryGetByIdAsync(id, ct);
            if (token is null)
                throw new NotFoundException();

            return token;
        }


        // Also calls to get by token hash. may be deprecated later.
        public Task<RefreshToken?> TryGetByTokenAsync(string tokenHash, CancellationToken ct = default)
        {
            return TryGetByTokenHashAsync(tokenHash, ct);
        }

        public async Task<RefreshToken> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default)
        {
            RefreshToken? existing = await TryGetByTokenHashAsync(tokenHash, ct);
            if (existing is null)
                throw new NotFoundException();

            return existing;
        }

        public async Task<RefreshToken?> TryGetByTokenHashAsync(string tokenHash, CancellationToken ct = default)
        {
            return await _refreshTokenRepository.GetByTokenHashAsync(tokenHash, ct);
        }

        public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken, CancellationToken ct = default)
        {
            await _refreshTokenRepository.AddAsync(refreshToken, ct);
            return refreshToken;
        }

        public async Task RevokeAllByUserIdAsync(Guid userId, string? ipAddress, string? reason, CancellationToken ct = default)
        {
            await _refreshTokenRepository.RevokeAllByUserIdAsync(userId, ipAddress ?? string.Empty, reason ?? "Revoked", ct);
        }

        public async Task<RefreshToken> RevokeAsync(
            string tokenHash,
            string? ipAddress,
            string? reason,
            string? replacedByTokenHash = null,
            CancellationToken ct = default)
        {
            RefreshToken? refreshToken = await GetByTokenHashAsync(tokenHash, ct);

            if (refreshToken.IsRevoked)
                return refreshToken;

            refreshToken.Revoke(
                ipAddress: ipAddress ?? string.Empty,
                reason: string.IsNullOrWhiteSpace(reason) ? "Revoked" : reason!,
                replacedByToken: replacedByTokenHash);

            refreshToken = await _refreshTokenRepository.UpdateAsync(refreshToken, ct);
            return refreshToken;
        }


        public async Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default)
        {
            RefreshToken refreshToken = await GetByIdAsync(id, ct);
            await _refreshTokenRepository.DeleteAsync(refreshToken, isSoftDelete, ct);
        }
    }
}
