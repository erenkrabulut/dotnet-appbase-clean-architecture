namespace core.Application.Features.RolePermissions.Dtos
{
    public sealed class RolePermissionDto
    {
        public Guid RoleId { get; init; }
        public int PermissionId { get; init; }
        public string PermissionName { get; init; } = string.Empty;
    }
}
