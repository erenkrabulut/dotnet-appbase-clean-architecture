namespace core.Application.Features.UserRoles.Dtos
{
    public sealed class UserRoleDto
    {
        public Guid UserId { get; init; }
        public Guid RoleId { get; init; }
        public string RoleName { get; init; } = string.Empty;
    }
}
