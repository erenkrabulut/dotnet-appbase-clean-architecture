using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Security.ExternalLoginService;
using core.Application.Abstractions.Security.Hashing;
using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Security.UserContext;
using core.Application.Abstractions.Transactions;
using core.Infrastructure.Logging;
using core.Infrastructure.Security.ExternalAuthService;
using core.Infrastructure.Security.Hashing;
using core.Infrastructure.Security.Tokens;
using core.Infrastructure.Security.UserContext;
using core.Infrastructure.Transactions;
using core.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
             Action<JwtOptions>? configureJwtOptions = null)
        {

            if (configureJwtOptions != null)
            {
                services.Configure(configureJwtOptions);
            }

            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddSingleton<ITokenService, JwtTokenService>();
            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IExternalAuthService, GoogleAuthService>();

            services.AddSingleton<ILogContextAccessor, HttpLogContextAccessor>();
            services.AddSingleton<ILoggerService, LoggerService>();

            services.AddScoped<IUnitOfWork, EfUnitOfWork<BaseDbContext>>();

            return services;
        }
    }
}
