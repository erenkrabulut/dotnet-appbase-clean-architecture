using core.Domain.Security;

namespace core.Application.Features.UserLogins.Dtos
{
    public sealed class UserLoginDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }

        public AuthenticationProvider Provider { get; init; }

        public string ProviderKey { get; init; } = string.Empty;

        public string? ProviderValue { get; init; }
    }
}
