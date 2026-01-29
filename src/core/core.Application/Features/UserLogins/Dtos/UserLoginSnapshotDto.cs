using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserLogins.Dtos
{
    public sealed class UserLoginsSnapshotDto
    {
        public Guid UserId { get; init; }
        public List<UserLoginDto> Logins { get; init; } = new();
    }
}
