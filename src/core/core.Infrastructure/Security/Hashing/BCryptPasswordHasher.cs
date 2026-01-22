using core.Application.Abstractions.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
