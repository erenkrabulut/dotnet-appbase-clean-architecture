using core.Application.Abstractions.Services.Seed;
using core.Application.Common.Security;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Seed
{
    public sealed class PermissionSeeder : ISeeder
    {
        public int Order => 10;

        private readonly BaseDbContext _db;

        public PermissionSeeder(BaseDbContext db)
        {
            _db = db;
        }

        public async Task SeedAsync(CancellationToken ct = default)
        {
            var desired = PermissionCatalog.GetAll();

            var existing = await _db.Set<Permission>()
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
                _db.Set<Permission>().Add(new Permission(name));
            }

            await _db.SaveChangesAsync(ct);
        }
    }
}