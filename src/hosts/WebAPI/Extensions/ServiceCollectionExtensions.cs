using core.Application;
using core.Infrastructure;
using core.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddControllers()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            services.AddHttpContextAccessor();
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerConfiguration();
            
            services.AddApiCors(configuration);
            services.AddApiRateLimiting(configuration);
            services.AddApiHealthChecks();
            services.AddJwtAuthentication(configuration, environment);

            return services;
        }
    }
}
