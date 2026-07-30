namespace core.Application.Abstractions.Services.Seed
{
    public interface IMigrationApplier
    {
        Task ApplyAsync(CancellationToken ct = default);
    }
}
