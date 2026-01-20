using core.Application.Repositories.Identity;
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

        public Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return _set.FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
        }
    }
}
