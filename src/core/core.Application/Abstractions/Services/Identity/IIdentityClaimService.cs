using core.Application.Abstractions.Security.IdentityClaims;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IIdentityClaimsService
    {
        Task<IdentityClaimsSnapshot> GetSnapshotAsync(Guid userId, CancellationToken ct = default);
    }


}
