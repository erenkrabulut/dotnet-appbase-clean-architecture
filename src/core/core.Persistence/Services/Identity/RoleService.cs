using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Domain.Entities.Identity;
using core.Domain.Errors;
using core.Persistence.Repositories.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PageResponse<Role>> GetPageAsync(PageRequest pageRequest, CancellationToken ct = default)
        {
            IQueryable<Role> query = _roleRepository.Query().AsNoTracking();

            query = ApplyOrderBy(query, pageRequest);

            int totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip(pageRequest.PageIndex * pageRequest.PageSize)
                .Take(pageRequest.PageSize)
                .ToListAsync(ct);

            return new PageResponse<Role>
            {
                Items = items,
                PageIndex = pageRequest.PageIndex,
                PageSize = pageRequest.PageSize,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Role> ApplyOrderBy(IQueryable<Role> query, PageRequest pageRequest)
        {
            if (string.IsNullOrWhiteSpace(pageRequest.OrderBy))
                return query.OrderBy(x => x.Id);

            string orderBy = pageRequest.OrderBy.Trim();

            return (orderBy, pageRequest.Desc) switch
            {
                (nameof(Role.Name), false) => query.OrderBy(x => x.Name),
                (nameof(Role.Name), true) => query.OrderByDescending(x => x.Name),

                (nameof(User.Id), false) => query.OrderBy(x => x.Id),
                (nameof(User.Id), true) => query.OrderByDescending(x => x.Id),

                _ when pageRequest.Desc => query.OrderByDescending(x => x.Id),
                _ => query.OrderBy(x => x.Id)
            };
        }
    }
}
