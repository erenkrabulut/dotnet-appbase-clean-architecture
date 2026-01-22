using core.Application.Abstractions.Security.ExternalAuthService;
using core.Application.Abstractions.Security.ExternalLoginService;
using core.Application.Abstractions.Security.Token;
using core.Application.Repositories.Identity;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Infrastructure.Security.ExternalAuthService
{

    /// <summary>
    /// UPDATE to Replace UserRepository with UserService to decouple implementation of Clean Arch.
    /// </summary>
    public class GoogleAuthService : IExternalAuthService
    {
        private readonly IUserRepository _userRepository;

        public GoogleAuthService(IUserRepository externalUserService)
        {
            _userRepository = externalUserService;
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
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

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
