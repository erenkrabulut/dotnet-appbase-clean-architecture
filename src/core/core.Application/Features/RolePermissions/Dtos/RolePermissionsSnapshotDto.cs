using core.Application.Features.Permissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.RolePermissions.Dtos
{
    public sealed class RolePermissionsSnapshotDto
    {
        public Guid RoleId { get; init; }
        public List<PermissionDto> Permissions { get; init; } = new();
    }
}
