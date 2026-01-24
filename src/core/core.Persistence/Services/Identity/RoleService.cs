using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Domain.Entities.Identity;
using core.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Services.Identity
{
    public sealed class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<Role?> TryGetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _roleRepository.GetAsync(r => r.Id == id, cancellationToken: ct);
        }

        public async Task<Role> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            Role? role = await TryGetByIdAsync(id, ct);
            if (role is null)
                throw new NotFoundException();

            return role;
        }

        public Task<Role?> TryGetByNameAsync(string name, CancellationToken ct = default)
        {
            return _roleRepository.GetByNameAsync(name, ct);
        }

        public async Task<Role> GetByNameAsync(string name, CancellationToken ct = default)
        {
            Role? role = await TryGetByNameAsync(name, ct);
            if (role is null)
                throw new NotFoundException();

            return role;
        }

        public async Task EnsureNameUniqueAsync(string name, CancellationToken ct = default)
        {
            Role? existing = await TryGetByNameAsync(name, ct);
            if (existing is not null)
                throw new ConflictException();
        }

        public async Task<Role> CreateAsync(Role role, CancellationToken ct = default)
        {
            await _roleRepository.AddAsync(role, ct);
            return role;
        }

        public Task UpdateAsync(Role role, CancellationToken ct = default)
        {
            return _roleRepository.UpdateAsync(role, ct);
        }

        public async Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default)
        {
            Role role = await GetByIdAsync(id, ct);
            await _roleRepository.DeleteAsync(role, isSoftDelete, ct);
        }
    }
}
