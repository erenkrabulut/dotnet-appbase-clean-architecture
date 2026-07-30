using core.Domain.Entities.Identity;

namespace core.Application.Abstractions.Security.Tokens
{
    public sealed record RefreshTokenResult(
        string RawToken,
        string TokenHash,
        DateTime ExpiresAt,
        RefreshToken Entity);
}
