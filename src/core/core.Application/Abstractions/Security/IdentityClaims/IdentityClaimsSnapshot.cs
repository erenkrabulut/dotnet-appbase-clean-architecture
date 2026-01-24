using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Security.IdentityClaims
{
    public sealed record IdentityClaimsSnapshot(
        IReadOnlyList<string> Roles,
        IReadOnlyList<string> Permissions);
}
