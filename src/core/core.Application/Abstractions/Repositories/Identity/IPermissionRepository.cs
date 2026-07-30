using core.Domain.Entities.Identity;

namespace core.Application.Abstractions.Repositories.Identity
{
    public interface IPermissionRepository : IRepository<Permission, int>
    {
        Task<Permission?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
