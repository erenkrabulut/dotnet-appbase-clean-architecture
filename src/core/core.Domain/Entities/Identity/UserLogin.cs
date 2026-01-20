using core.Domain.Common;
using core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Entities.Identity
{
    public class UserLogin: Entity<Guid>
    {
        public Guid UserId { get; set; }
        public AuthenticationProvider Provider { get; set; }

        public string ProviderKey { get; set; }
        public string? ProviderValue { get; set; }

        public virtual User User { get; set; } = null!;

        public UserLogin(Guid userId, AuthenticationProvider provider, string providerKey, string? providerValue)
        {
            UserId = userId;
            Provider = provider;
            ProviderKey = providerKey;
            ProviderValue = providerValue;
        }
    }
}
