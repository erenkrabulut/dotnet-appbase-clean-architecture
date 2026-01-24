using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Security.Tokens
{
    public sealed record RefreshTokenResult(
        string RawToken,
        string TokenHash,
        DateTime ExpiresAt,
        RefreshToken Entity);
}
