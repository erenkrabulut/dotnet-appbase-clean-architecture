using core.Domain.Entities.Identity;

namespace core.Application.Abstractions.Repositories.Identity
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
