using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Security.Tokens
{
    public sealed class AccessToken
    {
        public string Token { get; }
        public DateTime Expires { get; }

        public AccessToken(string token, DateTime expiresAt)
        {
            Token = token;
            Expires = expiresAt;
        }
    }
}
