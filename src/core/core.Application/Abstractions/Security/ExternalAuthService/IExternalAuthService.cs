using core.Application.Abstractions.Security.ExternalAuthService;

namespace core.Application.Abstractions.Security.ExternalLoginService
{
    public interface IExternalAuthService
    {
        Task<ExternalAuthResult> GoogleLoginAsync(string idToken, string ipAddress, CancellationToken cancellationToken = default);
    }
}
