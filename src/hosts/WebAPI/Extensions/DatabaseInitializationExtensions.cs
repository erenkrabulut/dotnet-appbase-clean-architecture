using core.Application.Abstractions.Services.Seed;

namespace WebAPI.Extensions
{
    public static class DatabaseInitializationExtensions
    {
        public static async Task InitializeDatabaseAsync(
            this WebApplication app,
            IConfiguration configuration,
            CancellationToken ct = default)
        {
            bool autoMigrate = configuration.GetValue("Database:AutoMigrate", true);
            bool autoSeed = configuration.GetValue("Database:AutoSeed", true);

            using var scope = app.Services.CreateScope();

            if (autoMigrate)
            {
                var migrator = scope.ServiceProvider.GetRequiredService<IMigrationApplier>();
                await migrator.ApplyAsync(ct);
            }

            if (autoSeed)
            {
                var seeder = scope.ServiceProvider.GetRequiredService<ISeedApplier>();
                await seeder.ApplyAsync(ct);
            }
        }
    }
}
