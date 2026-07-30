using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Domain.Entities.Identity;
using core.Domain.Errors;
using core.Domain.Security;
using Microsoft.EntityFrameworkCore;

namespace core.Persistence.Services.Identity
{
    public sealed class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly IUserRepository _userRepository;

        public UserLoginService(IUserLoginRepository userLoginRepository, IUserRepository userRepository)
        {
            _userLoginRepository = userLoginRepository;
            _userRepository = userRepository;
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

        public async Task<List<UserLogin>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            var userLogins = await _userLoginRepository.Query().AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync(ct);

            return userLogins;
        }

        public async Task<UserLogin?> TryGetByUserAndProviderAsync(Guid userId, AuthenticationProvider provider, CancellationToken ct = default)
        {
            return await _userLoginRepository.Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Provider == provider, ct);
        }

        public async Task EnsureProviderKeyUniqueAsync(AuthenticationProvider provider, string providerKey, Guid currentUserId, CancellationToken ct = default)
        {
            bool exists = await _userLoginRepository.Query()
                .AsNoTracking()
                .AnyAsync(x => x.Provider == provider && x.ProviderKey == providerKey && x.UserId != currentUserId, ct);

            if (exists)
                throw new ConflictException(IdentityErrors.UserLogin.ProviderKeyAlreadyExists);
        }

        public async Task LinkAsync(Guid userId, AuthenticationProvider provider, string providerKey, string? providerValue, CancellationToken ct = default)
        {
            bool userExists = await _userRepository.Query().AsNoTracking().AnyAsync(x => x.Id == userId, ct);
            if (!userExists)
                throw new NotFoundException(IdentityErrors.User.NotFound);

            await EnsureProviderKeyUniqueAsync(provider, providerKey, userId, ct);

            var existing = await _userLoginRepository.GetAsync(x => x.UserId == userId && x.Provider == provider, ct);

            if (existing is null)
            {
                await _userLoginRepository.AddAsync(new UserLogin(userId, provider, providerKey, providerValue), ct);
                return;
            }

            existing.ProviderKey = providerKey;
            existing.ProviderValue = providerValue;

            await _userLoginRepository.UpdateAsync(existing, ct);
        }

        public async Task UnlinkAsync(Guid userId, AuthenticationProvider provider, CancellationToken ct = default)
        {
            bool userExists = await _userRepository.Query().AsNoTracking().AnyAsync(x => x.Id == userId, ct);
            if (!userExists)
                throw new NotFoundException(IdentityErrors.User.NotFound);

            var existing = await _userLoginRepository.GetAsync(x => x.UserId == userId && x.Provider == provider, ct);
            if (existing is null)
                return;

            await _userLoginRepository.DeleteAsync(existing, isSoftDelete: false, cancellationToken: ct);
        }
    }
}
