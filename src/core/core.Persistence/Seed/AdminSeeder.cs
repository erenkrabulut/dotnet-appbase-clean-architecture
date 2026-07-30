using core.Application.Abstractions.Security.Hashing;
using core.Application.Abstractions.Services.Seed;
using core.Domain.Constants;
using core.Domain.Entities.Identity;
using core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace core.Persistence.Seed
{
    public sealed class AdminSeeder : ISeeder
    {

        public int Order => 100;

        private readonly BaseDbContext _db;
        private readonly IPasswordHasher _passwordHasher;

        public AdminSeeder(BaseDbContext db, IPasswordHasher passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }

        public async Task SeedAsync(CancellationToken ct = default)
        {
            bool exists = await _db.Set<User>()
                .IgnoreQueryFilters()
                .AnyAsync(x => x.Email == DefaultAdminDefaults.Email, ct);

            if (exists)
                return;

            var adminRole = await _db.Set<Role>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Name == RoleNames.Admin, ct);

            if (adminRole is null)
                return;

            string passwordHash = _passwordHasher.Hash(DefaultAdminDefaults.Password);

            var user = new User(
                firstName: DefaultAdminDefaults.FirstName,
                lastName: DefaultAdminDefaults.LastName,
                email: DefaultAdminDefaults.Email,
                passwordHash: passwordHash)
            {
                IsActive = true
            };

            _db.Set<User>().Add(user);

            _db.Set<UserRole>().Add(new UserRole(userId: user.Id, roleId: adminRole.Id));

            await _db.SaveChangesAsync(ct);
        }
    }
}
