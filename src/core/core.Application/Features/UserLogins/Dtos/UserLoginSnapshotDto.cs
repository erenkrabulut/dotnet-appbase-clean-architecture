namespace core.Application.Features.UserLogins.Dtos
{
    public sealed class UserLoginsSnapshotDto
    {
        public Guid UserId { get; init; }
        public List<UserLoginDto> Logins { get; init; } = new();
    }
}
