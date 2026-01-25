using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Security.ExternalAuthService
{
    public sealed class ExternalAuthResult
    {
        public bool Succeeded { get; init; }
        public ExternalAuthStatus Status { get; init; }

        public Guid? UserId { get; init; }
        public string? Email { get; init; }
        public string? Provider { get; init; }
        public string? ProviderKey { get; init; }

        public string? Error { get; init; }

        private ExternalAuthResult() { }

        public static ExternalAuthResult SuccessExistingUser(
            Guid userId,
            string email,
            string provider,
            string providerKey)
            => new()
            {
                Succeeded = true,
                Status = ExternalAuthStatus.ExistingUser,
                UserId = userId,
                Email = email,
                Provider = provider,
                ProviderKey = providerKey
            };

        public static ExternalAuthResult SuccessNewUser(
            string email,
            string provider,
            string providerKey)
            => new()
            {
                Succeeded = true,
                Status = ExternalAuthStatus.NewUserRequired,
                Email = email,
                Provider = provider,
                ProviderKey = providerKey
            };

        public static ExternalAuthResult Failed(string error)
            => new()
            {
                Succeeded = false,
                Status = ExternalAuthStatus.Failed,
                Error = error
            };
    }
}
