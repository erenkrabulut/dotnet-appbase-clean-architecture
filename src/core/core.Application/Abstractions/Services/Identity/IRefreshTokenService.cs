using core.Application.Common.Responses;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken?> TryGetByIdAsync(Guid id, CancellationToken ct = default);
        Task<RefreshToken> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<RefreshToken?> TryGetByTokenAsync(string token, CancellationToken ct = default);
        Task<RefreshToken> GetByTokenAsync(string token, CancellationToken ct = default);

        Task<RefreshToken?> TryGetByTokenHashAsync(string tokenHash, CancellationToken ct = default);

        Task<RefreshToken> CreateAsync(RefreshToken refreshToken, CancellationToken ct = default);

        Task<RefreshToken> RevokeAsync(
            string tokenHash,
            string? ipAddress,
            string? reason,
            string? replacedByTokenHash = null,
            CancellationToken ct = default);

        Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default);
    }
}
