using core.Application.Features.Roles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserRoles.Dtos
{
    public sealed class UserRolesSnapshotDto
    {
        public Guid UserId { get; init; }
        public List<RoleDto> Roles { get; init; } = new();
    }
}
