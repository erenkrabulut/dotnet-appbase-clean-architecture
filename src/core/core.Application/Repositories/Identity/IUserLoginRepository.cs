using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Repositories.Identity
{
    public interface IUserLoginRepository : IRepository<UserLogin, Guid>
    {
        Task<UserLogin?> GetByProviderAsync(
            Guid userId,
            string providerKey,
            CancellationToken cancellationToken = default
        );
    }
}
