using core.Application.Abstractions.Security.Tokens;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Security.Token
{
    public interface ITokenService
    {
        AccessToken CreateAccessToken(JwtClaims claims);
        RefreshTokenResult CreateRefreshToken(Guid userId, string ipAddress);
        string HashRefreshToken(string rawToken);
    }
}
