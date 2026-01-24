using core.Application.Abstractions.Repositories.Identity;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Repositories.Identity
{
    public class RefreshTokenRepository
        : EFRepository<RefreshToken, Guid, BaseDbContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(BaseDbContext context) : base(context) { }

        public async Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Set<RefreshToken>()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default)
        {
            return await GetByTokenHashAsync(token, ct);
        }

        public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default)
        {
            return await _context.Set<RefreshToken>()
                .FirstOrDefaultAsync(x => x.Token == tokenHash, ct);
        }
    }
}
