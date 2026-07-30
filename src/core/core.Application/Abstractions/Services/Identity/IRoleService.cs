using core.Application.Common.Paging;
using core.Domain.Entities.Identity;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IRoleService
    {
        Task<Role?> TryGetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Role> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<Role?> TryGetByNameAsync(string name, CancellationToken ct = default);
        Task<Role> GetByNameAsync(string name, CancellationToken ct = default);

        Task EnsureNameUniqueAsync(string name, CancellationToken ct = default);

        Task<Role> CreateAsync(Role role, CancellationToken ct = default);
        Task UpdateAsync(Role role, CancellationToken ct = default);

        Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default);

        Task<PageResponse<Role>> GetPageAsync(PageRequest pageRequest, CancellationToken ct = default);
    }
}
