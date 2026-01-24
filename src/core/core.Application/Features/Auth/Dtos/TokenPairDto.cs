using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Dtos
{
    public sealed class TokenPairDto
    {
        public string AccessToken { get; init; } = null!;
        public DateTime AccessTokenExpiresAt { get; init; }

        public string RefreshToken { get; init; } = null!;
        public DateTime RefreshTokenExpiresAt { get; init; }

        public TokenPairDto(
            string accessToken,
            DateTime accessTokenExpiresAt,
            string refreshToken,
            DateTime refreshTokenExpiresAt)
        {
            AccessToken = accessToken;
            AccessTokenExpiresAt = accessTokenExpiresAt;
            RefreshToken = refreshToken;
            RefreshTokenExpiresAt = refreshTokenExpiresAt;
        }
    }
}
