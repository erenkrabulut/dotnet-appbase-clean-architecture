using core.Domain.Common;
using core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {

        protected IConfiguration Configuration { get; set; }


        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public BaseDbContext(DbContextOptions options, IConfiguration configuration) 
            : base(options) 
        { 
            Configuration = configuration;    
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ApplySoftDeleteFilter(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(BaseDbContext)
                        .GetMethod(nameof(SetSoftDeleteFilter),
                                   BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, new object[] { modelBuilder });
                }
            }
        }

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder)
             where TEntity : class, ISoftDelete
        {
            modelBuilder.Entity<TEntity>()
                .HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
    }
}
