using core.Application.Features.Roles.Dtos;

namespace core.Application.Features.UserRoles.Dtos
{
    public sealed class UserRolesSnapshotDto
    {
        public Guid UserId { get; init; }
        public List<RoleDto> Roles { get; init; } = new();
    }
}
