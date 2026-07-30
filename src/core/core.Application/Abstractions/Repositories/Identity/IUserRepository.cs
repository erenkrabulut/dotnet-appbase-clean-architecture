using core.Domain.Entities.Identity;

namespace core.Application.Abstractions.Repositories.Identity
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
