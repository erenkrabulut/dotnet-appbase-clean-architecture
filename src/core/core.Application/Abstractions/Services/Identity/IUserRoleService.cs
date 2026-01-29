using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IUserRoleService
    {
        Task<IReadOnlyList<Role>> GetRolesByUserIdAsync(Guid userId, CancellationToken ct = default);

        Task<bool> IsRoleAssignedToUserAsync(Guid userId, Guid roleId, CancellationToken ct = default);

        Task AddRoleToUserAsync(Guid userId, Guid roleId, CancellationToken ct = default);

        Task RemoveRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken ct = default);

        Task ReplaceUserRolesAsync(Guid userId, IReadOnlyCollection<Guid> roleIds, CancellationToken ct = default);
    }
}
