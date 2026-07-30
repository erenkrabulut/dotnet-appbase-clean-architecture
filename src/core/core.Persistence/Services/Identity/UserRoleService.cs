using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Domain.Entities.Identity;
using core.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace core.Persistence.Services.Identity
{
    public sealed class UserRoleService : IUserRoleService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IReadOnlyList<Role>> GetRolesByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            var roles = await (
                from ur in _userRoleRepository.Query().AsNoTracking()
                join r in _roleRepository.Query().AsNoTracking() on ur.RoleId equals r.Id
                where ur.UserId == userId
                select r
            )
            .ToListAsync(ct);

            return roles;
        }

        public async Task<bool> IsRoleAssignedToUserAsync(Guid userId, Guid roleId, CancellationToken ct = default)
        {
            return await _userRoleRepository.Query()
                .AsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.RoleId == roleId, ct);
        }

        public async Task AddRoleToUserAsync(Guid userId, Guid roleId, CancellationToken ct = default)
        {
            bool userExists = await _userRepository.Query().AsNoTracking().AnyAsync(x => x.Id == userId, ct);
            if (!userExists)
                throw new NotFoundException(IdentityErrors.User.NotFound);

            bool roleExists = await _roleRepository.Query().AsNoTracking().AnyAsync(x => x.Id == roleId, ct);
            if (!roleExists)
                throw new NotFoundException(IdentityErrors.Role.NotFound);

            bool alreadyAssigned = await _userRoleRepository.Query()
                .AsNoTracking()
                .AnyAsync(x => x.UserId == userId && x.RoleId == roleId, ct);

            if (alreadyAssigned)
                return;

            await _userRoleRepository.AddAsync(new UserRole(userId, roleId), ct);
        }

        public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken ct = default)
        {
            bool userExists = await _userRepository.Query().AsNoTracking().AnyAsync(x => x.Id == userId, ct);
            if (!userExists)
                throw new NotFoundException(IdentityErrors.User.NotFound);

            bool roleExists = await _roleRepository.Query().AsNoTracking().AnyAsync(x => x.Id == roleId, ct);
            if (!roleExists)
                throw new NotFoundException(IdentityErrors.Role.NotFound);

            var userRole = await _userRoleRepository.GetAsync(
                x => x.UserId == userId && x.RoleId == roleId,
                ct);

            if (userRole is null)
                return; // idempotent remove

            await _userRoleRepository.DeleteAsync(userRole, isSoftDelete: false, cancellationToken: ct);
        }

        public async Task ReplaceUserRolesAsync(Guid userId, IReadOnlyCollection<Guid> roleIds, CancellationToken ct = default)
        {
            bool userExists = await _userRepository.Query().AsNoTracking().AnyAsync(x => x.Id == userId, ct);
            if (!userExists)
                throw new NotFoundException(IdentityErrors.User.NotFound);

            Guid[] distinctRoleIds = roleIds.Distinct().ToArray();

            // validate all roles exist
            var existingRoleIds = await _roleRepository.Query()
                .AsNoTracking()
                .Where(r => distinctRoleIds.Contains(r.Id))
                .Select(r => r.Id)
                .ToListAsync(ct);

            if (existingRoleIds.Count != distinctRoleIds.Length)
                throw new NotFoundException(IdentityErrors.Role.NotFound);

            var currentRoleIds = await _userRoleRepository.Query()
                .AsNoTracking()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync(ct);

            var toRemove = currentRoleIds.Except(distinctRoleIds).ToArray();
            var toAdd = distinctRoleIds.Except(currentRoleIds).ToArray();

            if (toRemove.Length > 0)
            {
                var removeEntities = await _userRoleRepository.Query()
                    .Where(x => x.UserId == userId && toRemove.Contains(x.RoleId))
                    .ToListAsync(ct);

                await _userRoleRepository.DeleteRangeAsync(removeEntities, isSoftDelete: false, cancellationToken: ct);
            }

            if (toAdd.Length > 0)
            {
                var addEntities = toAdd.Select(roleId => new UserRole(userId, roleId)).ToList();

                await _userRoleRepository.AddRangeAsync(addEntities, ct);
            }
        }
    }
}
