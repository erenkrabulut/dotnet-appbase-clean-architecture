using System.Threading.RateLimiting;

namespace WebAPI.Extensions
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddApiRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            var permitLimit = configuration.GetValue<int>("RateLimiting:PermitLimit", 100);
            var windowMinutes = configuration.GetValue<int>("RateLimiting:WindowMinutes", 1);

            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    // Use IP address for anonymous users, User identity for authenticated users.
                    var partitionKey = httpContext.User.Identity?.IsAuthenticated == true
                        ? httpContext.User.Identity.Name!
                        : (httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown");

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: partitionKey,
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = permitLimit,
                            QueueLimit = 0,
                            Window = TimeSpan.FromMinutes(windowMinutes)
                        });
                });

                // Return 429 instead of 503
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            return services;
        }
    }
}
