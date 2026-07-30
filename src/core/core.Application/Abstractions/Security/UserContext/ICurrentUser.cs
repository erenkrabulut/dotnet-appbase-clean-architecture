namespace core.Application.Abstractions.Security.UserContext
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
        IReadOnlyCollection<string> Roles { get; }
        IReadOnlyCollection<string> Permissions { get; }
        bool IsAuthenticated { get; }
    }
}
