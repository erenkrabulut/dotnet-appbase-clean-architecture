using core.Application.Abstractions.Services.Seed;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace core.Persistence.Seed
{
    public sealed class MigrationApplier : IMigrationApplier
    {
        private readonly BaseDbContext _db;

        public MigrationApplier(BaseDbContext db)
        {
            _db = db;
        }

        public async Task ApplyAsync(CancellationToken ct = default)
        {

            var pending = await _db.Database.GetPendingMigrationsAsync(ct);
            if (!pending.Any())
                return;

            await _db.Database.MigrateAsync(ct);

        }
    }

}
