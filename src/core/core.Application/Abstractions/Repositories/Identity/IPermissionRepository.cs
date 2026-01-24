using core.Application.Abstractions.Repositories;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Repositories.Identity
{
    public interface IPermissionRepository : IRepository<Permission, int>
    {
        Task<Permission?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
