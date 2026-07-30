using core.Application.Abstractions.Security.Tokens;

namespace core.Application.Abstractions.Security.Token
{
    public interface ITokenService
    {
        AccessToken CreateAccessToken(JwtClaims claims);
        RefreshTokenResult CreateRefreshToken(Guid userId, string ipAddress);
        string HashRefreshToken(string rawToken);
    }
}
