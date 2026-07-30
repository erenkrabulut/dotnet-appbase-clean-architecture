using core.Application;
using core.Infrastructure;
using core.Persistence;
using Serilog;
using WebAPI.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    // 1. Register Services
    builder.Services
        .AddApplicationServices()
        .AddPersistenceServices(builder.Configuration)
        .AddInfrastructureServices(jwt =>
        {
            builder.Configuration.GetSection("JwtOptions").Bind(jwt);
        })
        .AddApiServices(builder.Configuration, builder.Environment);

    var app = builder.Build();

    // 2. Database Initialization
    // Warning: AutoMigrate on startup is unsafe for multi-instance production environments.
    if (app.Environment.IsDevelopment() || builder.Configuration.GetValue("Database:AutoMigrate", false))
    {
        if (!app.Environment.IsDevelopment())
        {
            Log.Warning("Executing database migrations in non-development environment on startup. Consider moving this to a dedicated CI/CD step.");
        }
        await app.InitializeDatabaseAsync(builder.Configuration);
    }

    // 3. Configure Pipeline
    app.UseApiPipeline();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
