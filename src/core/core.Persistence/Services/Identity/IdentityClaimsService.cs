using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Security.IdentityClaims;
using core.Application.Abstractions.Services.Identity;
using Microsoft.EntityFrameworkCore;

namespace core.Persistence.Services.Identity
{
    public sealed class IdentityClaimsService : IIdentityClaimsService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IPermissionRepository _permissionRepository;

        public IdentityClaimsService(
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IRolePermissionRepository rolePermissionRepository,
            IPermissionRepository permissionRepository)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<IdentityClaimsSnapshot> GetSnapshotAsync(Guid userId, CancellationToken ct = default)
        {
            var roles = await (
                from ur in _userRoleRepository.Query().AsNoTracking()
                join r in _roleRepository.Query().AsNoTracking() on ur.RoleId equals r.Id
                where ur.UserId == userId
                select r.Name
            )
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToListAsync(ct);

            var permissions = await (
                from ur in _userRoleRepository.Query().AsNoTracking()
                join rp in _rolePermissionRepository.Query().AsNoTracking() on ur.RoleId equals rp.RoleId
                join p in _permissionRepository.Query().AsNoTracking() on rp.PermissionId equals p.Id
                where ur.UserId == userId
                select p.Name
            )
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToListAsync(ct);

            var roleSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in roles)
                roleSet.Add(r!);

            var permSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var p in permissions)
                permSet.Add(p!);

            return new IdentityClaimsSnapshot(
                Roles: roleSet.ToList(),
                Permissions: permSet.ToList());
        }
    }
}
