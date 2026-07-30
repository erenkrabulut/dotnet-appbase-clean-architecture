using core.Application.Abstractions.Services.Seed;
using core.Domain.Constants;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace core.Persistence.Seed
{
    public sealed class RoleSeeder : ISeeder
    {
        public int Order => 20;

        private readonly BaseDbContext _db;

        public RoleSeeder(BaseDbContext db)
        {
            _db = db;
        }

        public async Task SeedAsync(CancellationToken ct = default)
        {
            string[] desired = new[]
            {
                RoleNames.Admin,
                RoleNames.User
            };

            var existing = await _db.Set<Role>()
                .IgnoreQueryFilters()
                .Select(x => x.Name)
                .ToListAsync(ct);

            var missing = desired
                .Except(existing, StringComparer.Ordinal)
                .ToArray();

            if (missing.Length == 0)
                return;

            foreach (var name in missing)
            {
                _db.Set<Role>().Add(new Role(name));
            }

            await _db.SaveChangesAsync(ct);
        }
    }
}
