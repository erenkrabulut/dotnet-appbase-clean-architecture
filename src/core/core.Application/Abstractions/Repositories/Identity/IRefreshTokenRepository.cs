using core.Application.Abstractions.Repositories;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Repositories.Identity
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken, Guid>
    {
        Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default);

        Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default);
    }
}
