using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddApiCors(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    if (allowedOrigins.Length > 0)
                    {
                        policy.WithOrigins(allowedOrigins)
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    }
                    else
                    {
                        // Fallback/safe default if no origins specified (mostly block)
                        // This prevents AllowAnyOrigin from being used in production without thought.
                    }
                });
            });

            return services;
        }
    }
}
