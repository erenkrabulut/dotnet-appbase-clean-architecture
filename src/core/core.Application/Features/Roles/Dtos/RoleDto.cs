using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Dtos
{
    public sealed class RoleDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
