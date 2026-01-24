using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Services.Identity
{
    public sealed class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public Task<Permission?> TryGetByIdAsync(int id, CancellationToken ct = default)
        {
            return _permissionRepository.GetAsync(p => p.Id == id, cancellationToken: ct);
        }

        public async Task<Permission> GetByIdAsync(int id, CancellationToken ct = default)
        {
            Permission? permission = await TryGetByIdAsync(id, ct);
            if (permission is null)
                throw new NotFoundException();

            return permission;
        }

        public Task<Permission?> TryGetByNameAsync(string name, CancellationToken ct = default)
        {
            return _permissionRepository.GetByNameAsync(name, ct);
        }

        public async Task<Permission> GetByNameAsync(string name, CancellationToken ct = default)
        {
            Permission? permission = await TryGetByNameAsync(name, ct);
            if (permission is null)
                throw new NotFoundException();

            return permission;
        }

        public async Task EnsureNameUniqueAsync(string name, CancellationToken ct = default)
        {
            Permission? existing = await TryGetByNameAsync(name, ct);
            if (existing is not null)
                throw new ConflictException();
        }
    }
}
