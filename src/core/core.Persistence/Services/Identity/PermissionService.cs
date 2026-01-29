using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Paging;
using core.Domain.Entities.Identity;
using core.Persistence.Repositories.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PageResponse<Permission>> GetPageAsync(PageRequest pageRequest, CancellationToken ct = default)
        {
            IQueryable<Permission> query = _permissionRepository.Query().AsNoTracking();

            query = ApplyOrderBy(query, pageRequest);

            int totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip(pageRequest.PageIndex * pageRequest.PageSize)
                .Take(pageRequest.PageSize)
                .ToListAsync(ct);

            return new PageResponse<Permission>
            {
                Items = items,
                PageIndex = pageRequest.PageIndex,
                PageSize = pageRequest.PageSize,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Permission> ApplyOrderBy(IQueryable<Permission> query, PageRequest pageRequest)
        {
            if (string.IsNullOrWhiteSpace(pageRequest.OrderBy))
                return query.OrderBy(x => x.Id);

            string orderBy = pageRequest.OrderBy.Trim();

            return (orderBy, pageRequest.Desc) switch
            {
                (nameof(Permission.Name), false) => query.OrderBy(x => x.Name),
                (nameof(Permission.Name), true) => query.OrderByDescending(x => x.Name),

                (nameof(Permission.Id), false) => query.OrderBy(x => x.Id),
                (nameof(Permission.Id), true) => query.OrderByDescending(x => x.Id),

                _ when pageRequest.Desc => query.OrderByDescending(x => x.Id),
                _ => query.OrderBy(x => x.Id)
            };
        }
    }
}
