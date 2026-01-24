using core.Application.Common.Responses;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IPermissionService
    {
        Task<Permission?> TryGetByIdAsync(int id, CancellationToken ct = default);
        Task<Permission> GetByIdAsync(int id, CancellationToken ct = default);

        Task<Permission?> TryGetByNameAsync(string name, CancellationToken ct = default);
        Task<Permission> GetByNameAsync(string name, CancellationToken ct = default);

        Task EnsureNameUniqueAsync(string name, CancellationToken ct = default);
    }
}
