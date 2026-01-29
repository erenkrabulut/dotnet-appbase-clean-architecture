using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Permissions.Dtos
{
    public sealed class PermissionDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
