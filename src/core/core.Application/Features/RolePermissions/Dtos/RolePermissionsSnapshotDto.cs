using core.Application.Features.Permissions.Dtos;

namespace core.Application.Features.RolePermissions.Dtos
{
    public sealed class RolePermissionsSnapshotDto
    {
        public Guid RoleId { get; init; }
        public List<PermissionDto> Permissions { get; init; } = new();
    }
}
