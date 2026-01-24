using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Errors
{
    public enum ErrorType
    {
        Validation,
        Authorization,
        NotFound,
        Conflict,
        Business,
        External,
        Persistence,
        Internal,
        Identity
    }
}
