using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Domain.Entities.Identity;
using core.Domain.Errors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Services.Identity
{
    public sealed class RolePermissionService : IRolePermissionService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionService(
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IRolePermissionRepository rolePermissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(Guid roleId, CancellationToken ct = default)
        {
            var permissions = await (
                from rp in _rolePermissionRepository.Query().AsNoTracking()
                join p in _permissionRepository.Query().AsNoTracking() on rp.PermissionId equals p.Id
                where rp.RoleId == roleId
                select p
            )
            .ToListAsync(ct);

            return permissions;
        }

        public async Task<bool> IsPermissionAssignedToRoleAsync(Guid roleId, int permissionId, CancellationToken ct = default)
        {
            return await _rolePermissionRepository.Query()
                .AsNoTracking()
                .AnyAsync(x => x.RoleId == roleId && x.PermissionId == permissionId, ct);
        }

        public async Task AddPermissionToRoleAsync(Guid roleId, int permissionId, CancellationToken ct = default)
        {
            bool roleExists = await _roleRepository.Query().AsNoTracking().AnyAsync(x => x.Id == roleId, ct);
            if (!roleExists)
                throw new NotFoundException(IdentityErrors.Role.NotFound);

            bool permissionExists = await _permissionRepository.Query().AsNoTracking().AnyAsync(x => x.Id == permissionId, ct);
            if (!permissionExists)
                throw new NotFoundException(IdentityErrors.Permission.NotFound);

            bool alreadyAssigned = await _rolePermissionRepository.Query()
                .AsNoTracking()
                .AnyAsync(x => x.RoleId == roleId && x.PermissionId == permissionId, ct);

            if (alreadyAssigned)
                return; // prevent duplicates (idempotent)

            await _rolePermissionRepository.AddAsync(new RolePermission(roleId, permissionId), ct);
        }

        public async Task RemovePermissionFromRoleAsync(Guid roleId, int permissionId, CancellationToken ct = default)
        {
            bool roleExists = await _roleRepository.Query().AsNoTracking().AnyAsync(x => x.Id == roleId, ct);
            if (!roleExists)
                throw new NotFoundException(IdentityErrors.Role.NotFound);

            bool permissionExists = await _permissionRepository.Query().AsNoTracking().AnyAsync(x => x.Id == permissionId, ct);
            if (!permissionExists)
                throw new NotFoundException(IdentityErrors.Permission.NotFound);

            var rolePermission = await _rolePermissionRepository.GetAsync(
                x => x.RoleId == roleId && x.PermissionId == permissionId,
                ct);

            if (rolePermission is null)
                return; // idempotent remove

            await _rolePermissionRepository.DeleteAsync(rolePermission, isSoftDelete: false, cancellationToken: ct);
        }

        public async Task ReplaceRolePermissionsAsync(Guid roleId, IReadOnlyCollection<int> permissionIds, CancellationToken ct = default)
        {
            bool roleExists = await _roleRepository.Query().AsNoTracking().AnyAsync(x => x.Id == roleId, ct);
            if (!roleExists)
                throw new NotFoundException(IdentityErrors.Role.NotFound);

            int[] distinctPermissionIds = permissionIds.Distinct().ToArray();

            // validate all permissions exist
            var existingPermissionIds = await _permissionRepository.Query()
                .AsNoTracking()
                .Where(p => distinctPermissionIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync(ct);

            if (existingPermissionIds.Count != distinctPermissionIds.Length)
                throw new NotFoundException(IdentityErrors.Permission.NotFound);

            var currentPermissionIds = await _rolePermissionRepository.Query()
                .AsNoTracking()
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync(ct);

            var toRemove = currentPermissionIds.Except(distinctPermissionIds).ToArray();
            var toAdd = distinctPermissionIds.Except(currentPermissionIds).ToArray();

            if (toRemove.Length > 0)
            {
                var removeEntities = await _rolePermissionRepository.Query()
                    .Where(x => x.RoleId == roleId && toRemove.Contains(x.PermissionId))
                    .ToListAsync(ct);

                await _rolePermissionRepository.DeleteRangeAsync(removeEntities, isSoftDelete: false, cancellationToken: ct);
            }

            if (toAdd.Length > 0)
            {
                var addEntities = toAdd.Select(pid => new RolePermission(roleId, pid)).ToList();

                await _rolePermissionRepository.AddRangeAsync(addEntities, ct);
            }
        }
    }
}
