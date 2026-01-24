using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Security.ExternalAuthService;
using core.Application.Abstractions.Security.ExternalLoginService;
using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Services.Identity;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Google.Apis.Auth.GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await Google.Apis.Auth.GoogleJsonWebSignature.ValidateAsync(idToken);
            }
            catch (Exception ex)
            {
                return ExternalAuthResult.Failed($"Invalid Google token: {ex.Message}");
            }

            var email = payload.Email;
            var user = await _userService.TryGetByEmailAsync(email, cancellationToken);

            if (user != null)
            {
                return ExternalAuthResult.SuccessExistingUser(user.Id, email, "Google");
            }
            else
            {
                return ExternalAuthResult.SuccessNewUser(email, "Google");
            }
        }
    }
}
