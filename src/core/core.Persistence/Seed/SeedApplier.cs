using core.Application.Abstractions.Services.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Seed
{
    public sealed class SeedApplier : ISeedApplier
    {
        private readonly IEnumerable<ISeeder> _seeders;

        public SeedApplier(IEnumerable<ISeeder> seeders)
        {
            _seeders = seeders;
        }

        public async Task ApplyAsync(CancellationToken ct = default)
        {
            foreach (var seeder in _seeders.OrderBy(x => x.Order))
            {
                await seeder.SeedAsync(ct);
            }
        }
    }
}
