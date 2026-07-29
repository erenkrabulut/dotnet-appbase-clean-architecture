using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
        {
            var healthCheckBuilder = services.AddHealthChecks();
            
            // In a real application, you would add database checks here.
            // e.g., healthCheckBuilder.AddNpgSql(configuration.GetConnectionString("DefaultConnection"));
            // Since this is generic, we just register the base service.

            return services;
        }
    }
}
