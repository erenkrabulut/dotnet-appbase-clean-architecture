using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Abstractions.Transactions;
using core.Infrastructure.Transactions;
using core.Persistence.Contexts;
using core.Persistence.Repositories.Identity;
using core.Persistence.Services.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IUnitOfWork, EfUnitOfWork<BaseDbContext>>();

            // Repositories (Identity)
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

            // Services (Identity) - if you decided services live in Persistence
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<IIdentityClaimsService, IdentityClaimsService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();

            return services;
        }
    }
}
