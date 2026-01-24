using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Services.Identity
{
    public sealed class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository;

        public UserLoginService(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }

        public Task<UserLogin?> TryGetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _userLoginRepository.GetAsync(x => x.Id == id, cancellationToken: ct);
        }

        public async Task<UserLogin> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            UserLogin? login = await TryGetByIdAsync(id, ct);
            if (login is null)
                throw new NotFoundException();

            return login;
        }

        public Task<UserLogin?> TryGetByProviderAsync(Guid userId, string providerKey, CancellationToken ct = default)
        {
            return _userLoginRepository.GetByProviderAsync(userId, providerKey, ct);
        }

        public async Task<UserLogin> GetByProviderAsync(Guid userId, string providerKey, CancellationToken ct = default)
        {
            UserLogin? login = await TryGetByProviderAsync(userId, providerKey, ct);
            if (login is null)
                throw new NotFoundException();

            return login;
        }

        public async Task<UserLogin> CreateAsync(UserLogin userLogin, CancellationToken ct = default)
        {
            await _userLoginRepository.AddAsync(userLogin, ct);
            return userLogin;
        }

        public async Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default)
        {
            UserLogin userLogin = await GetByIdAsync(id, ct);
            await _userLoginRepository.DeleteAsync(userLogin, isSoftDelete, ct);
        }
    }
}
