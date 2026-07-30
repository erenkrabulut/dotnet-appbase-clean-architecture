using core.Domain.Entities.Identity;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IRolePermissionService
    {
        Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(Guid roleId, CancellationToken ct = default);

        Task<bool> IsPermissionAssignedToRoleAsync(Guid roleId, int permissionId, CancellationToken ct = default);

        Task AddPermissionToRoleAsync(Guid roleId, int permissionId, CancellationToken ct = default);

        Task RemovePermissionFromRoleAsync(Guid roleId, int permissionId, CancellationToken ct = default);

        Task ReplaceRolePermissionsAsync(Guid roleId, IReadOnlyCollection<int> permissionIds, CancellationToken ct = default);
    }
}
