namespace core.Application.Abstractions.Security.IdentityClaims
{
    public sealed record IdentityClaimsSnapshot(
        IReadOnlyList<string> Roles,
        IReadOnlyList<string> Permissions);
}
