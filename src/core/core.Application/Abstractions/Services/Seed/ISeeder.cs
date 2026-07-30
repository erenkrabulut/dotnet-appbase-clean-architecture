namespace core.Application.Abstractions.Services.Seed
{
    public interface ISeeder
    {
        int Order { get; }
        Task SeedAsync(CancellationToken ct = default);
    }
}
