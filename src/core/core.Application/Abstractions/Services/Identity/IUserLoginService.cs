using core.Application.Common.Responses;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Services.Identity
{
    public interface IUserLoginService
    {
        Task<UserLogin?> TryGetByIdAsync(Guid id, CancellationToken ct = default);
        Task<UserLogin> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<List<UserLogin>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);

        Task<UserLogin?> TryGetByProviderAsync(Guid userId, string providerKey, CancellationToken ct = default);
        Task<UserLogin> GetByProviderAsync(Guid userId, string providerKey, CancellationToken ct = default);

        Task<UserLogin> CreateAsync(UserLogin userLogin, CancellationToken ct = default);

        Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default);
    }
}
