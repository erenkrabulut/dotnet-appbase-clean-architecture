using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Errors
{
    public static class AuthErrors
    {
        public static readonly Error NotAuthenticated =
            new("AUTH.NOT_AUTHENTICATED", "User is not authenticated.", ErrorType.Authorization);

        public static readonly Error NotAuthorized =
            new("AUTH.NOT_AUTHORIZED", "User is not authorized for this operation.", ErrorType.Authorization);
    }
}
