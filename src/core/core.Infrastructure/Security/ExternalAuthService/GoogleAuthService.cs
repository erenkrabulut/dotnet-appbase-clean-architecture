using core.Application.Abstractions.Security.ExternalAuthService;
using core.Application.Abstractions.Security.ExternalLoginService;
using core.Application.Abstractions.Services.Identity;
using Google.Apis.Auth;

namespace core.Infrastructure.Security.ExternalAuthService
{

    public class GoogleAuthService : IExternalAuthService
    {
        private readonly IUserService _userService;

        public GoogleAuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ExternalAuthResult> GoogleLoginAsync(
            string idToken,
            string ipAddress,
            CancellationToken cancellationToken = default)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            }
            catch (Exception ex)
            {
                return ExternalAuthResult.Failed($"Invalid Google token: {ex.Message}");
            }

            var email = payload.Email;
            var providerKey = payload.Subject;

            var user = await _userService.TryGetByEmailAsync(email, cancellationToken);

            if (user != null)
            {
                return ExternalAuthResult.SuccessExistingUser(user.Id, email, "Google", providerKey);
            }

            return ExternalAuthResult.SuccessNewUser(email, "Google", providerKey);
        }
    }
}
