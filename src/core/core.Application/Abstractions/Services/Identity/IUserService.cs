using core.Application.Common.Responses;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IUserService
    {
        Task<User?> TryGetByIdAsync(Guid id, CancellationToken ct = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<User?> TryGetByEmailAsync(string email, CancellationToken ct = default);
        Task<User> GetByEmailAsync(string email, CancellationToken ct = default);

        Task EnsureEmailUniqueAsync(string email, CancellationToken ct = default);

        Task<User> CreateAsync(User user, CancellationToken ct = default);
        Task UpdateAsync(User user, CancellationToken ct = default);

        Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default);
    }
}
