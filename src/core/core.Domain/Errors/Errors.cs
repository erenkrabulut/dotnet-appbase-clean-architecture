using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Errors
{
    public static class Errors
    {
        public static class General
        {
            public static readonly Error Internal =
                new("GEN.INTERNAL", "Unexpected error occurred.", ErrorType.Internal);
        }

        public static class Auth
        {
            public static readonly Error NotAuthenticated =
                new("AUTH.NOT_AUTHENTICATED", "User is not authenticated.", ErrorType.Authorization);

            public static readonly Error NotAuthorized =
                new("AUTH.NOT_AUTHORIZED", "User is not authorized for this operation.", ErrorType.Authorization);
        }

        public static class Validation
        {
            public static readonly Error ValidationError =
                new("VALIDATION_ERROR", "Validation failed.", ErrorType.Validation);
        }
    }
}
