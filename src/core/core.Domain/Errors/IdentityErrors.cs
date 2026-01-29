using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Domain.Errors
{
    public sealed class IdentityErrors
    {
        public static class User
        {
            public static readonly Error NotFound =
                new("IDENTITY.USER_NOT_FOUND", "User is not found.", ErrorType.Identity);

            public static readonly Error EmailAlreadyExists =
                new("IDENTITY.EMAIL_ALREADY_EXISTS", "Email is already exists.", ErrorType.Identity);
        }

        public static class RefreshToken
        {
            public static readonly Error NotFound =
                new("IDENTITY.REFRESH_TOKEN_NOT_FOUND", "Refresh token is not found.", ErrorType.Identity);
        }

        public static class Role
        {
            public static readonly Error NotFound =
                new("IDENTITY.ROLE_NOT_FOUND", "Role is not found.", ErrorType.Identity);

            public static readonly Error NameAlreadyExists =
                new("IDENTITY.ROLE_NAME_ALREADY_EXISTS", "Role name already exists.", ErrorType.Identity);
        }

        public static class Permission
        {
            public static readonly Error NotFound =
                new("IDENTITY.PERMISSION_NOT_FOUND", "Permission is not found.", ErrorType.Identity);

            public static readonly Error NameAlreadyExists =
                new("IDENTITY.PERMISSION_NAME_ALREADY_EXISTS", "Permission name already exists.", ErrorType.Identity);
        }

        public static class UserRole
        {
            public static readonly Error RoleNotAssignedToUser =
                new("IDENTITY.USER_ROLE_NOT_ASSIGNED", "Role is not assigned to the user.", ErrorType.Identity);

            public static readonly Error AlreadyAssigned =
                new("IDENTITY.USER_ROLE_ALREADY_ASSIGNED", "Role is already assigned to the user.", ErrorType.Identity);
        }

        public static class RolePermission
        {
            public static readonly Error PermissionNotAssignedToRole =
                new("IDENTITY.ROLE_PERMISSION_NOT_ASSIGNED", "Permission is not assigned to the role.", ErrorType.Identity);

            public static readonly Error AlreadyAssigned =
                new("IDENTITY.ROLE_PERMISSION_ALREADY_ASSIGNED", "Permission is already assigned to the role.", ErrorType.Identity);
        }

        public static class UserLogin
        {
            public static readonly Error ProviderKeyAlreadyExists =
                new("IDENTITY.USER_LOGIN_PROVIDER_KEY_ALREADY_EXIST", "Provider Key is already exists.", ErrorType.Identity);
        }
    }
}
