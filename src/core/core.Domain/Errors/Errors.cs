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
                new("GENERAL.INTERNAL", "Unexpected error occurred.", ErrorType.Internal);

            public static readonly Error NotFound =
                new("GENERAL.NOT_FOUND", "Not found error occured.", ErrorType.NotFound);

            public static readonly Error Conflict =
                new("GENERAL.CONFLICT", "Conflict error occured.", ErrorType.Conflict);
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
                new("VALIDATION.VALIDATION_FAILED", "Validation failed.", ErrorType.Validation);
        }

        public static class Identity
        {
            public static readonly Error RefreshTokenNotFound =
                new("IDENTITY.REFRESH_TOKEN_NOT_FOUND", "Refresh token is not found.", ErrorType.Identity);

            public static readonly Error UserNotFound =
                new("IDENTITY.USER_NOT_FOUND", "User is not found.", ErrorType.Identity);
        }


    }
}
