using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using WebAPI.Middleware;

namespace WebAPI.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseApiPipeline(this WebApplication app)
        {
            // 1. Forwarded Headers (MUST be early to affect HTTPS redirection, auth, etc.)
            var forwardedOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwardedOptions.KnownIPNetworks.Clear();
            forwardedOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardedOptions);

            // 2. Swagger (Dev only)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 3. Serilog request logging
            app.UseSerilogRequestLogging();

            // 4. Security Headers
            app.UseMiddleware<SecurityHeadersMiddleware>();

            // 5. Https Redirection
            app.UseHttpsRedirection();

            // 6. CORS
            app.UseCors("AllowSpecificOrigins");

            // 7. Rate Limiter
            app.UseRateLimiter();

            // 8. Auth
            app.UseAuthentication();
            app.UseAuthorization();

            // 9. Endpoints
            app.MapControllers();

            app.MapHealthChecks("/health/live");
            app.MapHealthChecks("/health/ready"); // could be configured with distinct predicates

            return app;
        }
    }
}
