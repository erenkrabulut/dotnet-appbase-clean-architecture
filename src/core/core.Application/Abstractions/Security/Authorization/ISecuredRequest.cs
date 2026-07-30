namespace core.Application.Abstractions.Security.Authorization
{
    public interface ISecuredRequest
    {
        IReadOnlyCollection<string> Permissions { get; }
    }
}
