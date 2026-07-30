using core.Application.Abstractions.Security.Hashing;

namespace core.Infrastructure.Security.Hashing
{
    public sealed class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, string hash) =>
            BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
