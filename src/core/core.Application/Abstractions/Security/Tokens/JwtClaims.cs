using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Security.Tokens
{
    public sealed class JwtClaims
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = null!;
        public IReadOnlyList<string> Roles { get; init; } = Array.Empty<string>();
        public IReadOnlyList<string> Permissions { get; init; } = Array.Empty<string>();
    }
}
