using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Domain.Entities.Identity;
using core.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Services.Identity
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User?> TryGetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _userRepository.GetAsync(u => u.Id == id, cancellationToken: ct);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            User? user = await TryGetByIdAsync(id, ct);
            if (user is null)
                throw new NotFoundException();

            return user;
        }

        public Task<User?> TryGetByEmailAsync(string email, CancellationToken ct = default)
        {
            return _userRepository.GetByEmailAsync(email, ct);
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            User? user = await TryGetByEmailAsync(email, ct);
            if (user is null)
                throw new NotFoundException();

            return user;
        }

        public async Task EnsureEmailUniqueAsync(string email, CancellationToken ct = default)
        {
            User? existing = await TryGetByEmailAsync(email, ct);
            if (existing is not null)
                throw new ConflictException();
        }

        public async Task<User> CreateAsync(User user, CancellationToken ct = default)
        {
            await _userRepository.AddAsync(user, ct);
            return user;
        }

        public Task UpdateAsync(User user, CancellationToken ct = default)
        {
            return _userRepository.UpdateAsync(user, ct);
        }

        public async Task DeleteAsync(Guid id, bool isSoftDelete = true, CancellationToken ct = default)
        {
            User user = await GetByIdAsync(id, ct); 
            await _userRepository.DeleteAsync(user, isSoftDelete, ct);
        }
    }
}
