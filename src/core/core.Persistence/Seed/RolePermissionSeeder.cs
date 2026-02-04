using core.Application.Abstractions.Services.Seed;
using core.Application.Common.Security;
using core.Domain.Constants;
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
    public sealed class RolePermissionSeeder : ISeeder
    {
        public int Order => 30;

        private readonly BaseDbContext _db;

        public RolePermissionSeeder(BaseDbContext db)
        {
            _db = db;
        }

        public async Task SeedAsync(CancellationToken ct = default)
        {
            var roles = await _db.Set<Role>()
                .IgnoreQueryFilters()
                .Where(r => r.Name == RoleNames.Admin || r.Name == RoleNames.User)
                .ToListAsync(ct);

            var adminRole = roles.FirstOrDefault(x => x.Name == RoleNames.Admin);
            var userRole = roles.FirstOrDefault(x => x.Name == RoleNames.User);

            if (adminRole is null || userRole is null)
                return;

            var adminDesiredNames = PermissionCatalog.GetAdmins(); 
            var userDesiredNames = PermissionCatalog.GetReads();   

            if (adminDesiredNames.Length == 0 && userDesiredNames.Length == 0)
                return;

            
            var desiredNames = adminDesiredNames
                .Concat(userDesiredNames)
                .Distinct(StringComparer.Ordinal)
                .ToArray();

            var permissionIdByName = await _db.Set<Permission>()
                .Where(p => desiredNames.Contains(p.Name))
                .Select(p => new { p.Id, p.Name })
                .ToDictionaryAsync(x => x.Name, x => x.Id, StringComparer.Ordinal, ct);


            var desiredPairs = new List<(Guid RoleId,int PermissionId)>();

            foreach (var name in adminDesiredNames)
            {
                if (permissionIdByName.TryGetValue(name, out var permissionId))
                    desiredPairs.Add((adminRole.Id, permissionId));
            }

            foreach (var name in userDesiredNames)
            {
                if (permissionIdByName.TryGetValue(name, out var permissionId))
                    desiredPairs.Add((userRole.Id, permissionId));
            }

            if (desiredPairs.Count == 0)
                return;

            var roleIds = desiredPairs.Select(x => x.RoleId).Distinct().ToArray();
            var permissionIds = desiredPairs.Select(x => x.PermissionId).Distinct().ToArray();

            var existing = await _db.Set<RolePermission>()
                .IgnoreQueryFilters()
                .Where(x => roleIds.Contains(x.RoleId) && permissionIds.Contains(x.PermissionId))
                .Select(x => new { x.RoleId, x.PermissionId })
                .ToListAsync(ct);

            var existingSet = new HashSet<(Guid RoleId, int PermissionId)>(
                existing.Select(x => (x.RoleId, x.PermissionId))
            );

            var missing = desiredPairs
                .Where(x => !existingSet.Contains(x))
                .ToArray();

            if (missing.Length == 0)
                return;

            foreach (var (roleId, permissionId) in missing)
            {
                _db.Set<RolePermission>().Add(new RolePermission(roleId, permissionId));
            }

            await _db.SaveChangesAsync(ct);
        }
    }
}
