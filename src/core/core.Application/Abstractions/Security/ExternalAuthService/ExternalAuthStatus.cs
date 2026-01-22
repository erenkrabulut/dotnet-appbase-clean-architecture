using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Security.ExternalAuthService
{
    public enum ExternalAuthStatus
    {
        ExistingUser,
        NewUserRequired,
        Failed
    }
}
