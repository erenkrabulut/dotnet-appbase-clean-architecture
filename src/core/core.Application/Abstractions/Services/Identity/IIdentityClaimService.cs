using core.Application.Abstractions.Security.IdentityClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IIdentityClaimsService
    {
        Task<IdentityClaimsSnapshot> GetSnapshotAsync(Guid userId, CancellationToken ct = default);
    }


}
