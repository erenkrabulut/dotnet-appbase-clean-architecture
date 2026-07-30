namespace core.Application.Abstractions.Services.Seed
{
    public interface ISeedApplier
    {
        Task ApplyAsync(CancellationToken ct = default);
    }
}
