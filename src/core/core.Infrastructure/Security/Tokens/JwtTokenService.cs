using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Security.Tokens;
using core.Domain.Entities.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace core.Infrastructure.Security.Tokens
{
    public sealed class JwtTokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public AccessToken CreateAccessToken(JwtClaims claims)
        {
            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenMinutes);

            var tokenClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, claims.UserId.ToString()),
                new(JwtRegisteredClaimNames.Email, claims.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("typ", "access"),
                new(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            tokenClaims.AddRange(
                claims.Roles.Select(r => new Claim(ClaimTypes.Role, r))
             );

            tokenClaims.AddRange(
                claims.Permissions.Select( p => new Claim(CustomClaimTypes.Permission, p))
             );

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)
             );

            var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
            );

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                expires: expires,
                signingCredentials: credentials
            );


            var tokenValue = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AccessToken(tokenValue, expires);
        }

        public RefreshToken CreateRefreshToken(Guid userId, string ipAddress)
        {
            var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var hashed = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));
            var token = Convert.ToBase64String(hashed);
            var expires = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenTTL);

            return new RefreshToken( userId, token, expires, ipAddress );
        }
    }
}
