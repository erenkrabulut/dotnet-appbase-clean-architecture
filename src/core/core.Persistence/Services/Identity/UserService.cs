using core.Application.Abstractions.Repositories.Identity;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Paging;
using core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PageResponse<User>> GetPageAsync(PageRequest pageRequest, CancellationToken ct = default)
        {
            IQueryable<User> query = _userRepository.Query().AsNoTracking();

            query = ApplyOrderBy(query, pageRequest);

            int totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip(pageRequest.PageIndex * pageRequest.PageSize)
                .Take(pageRequest.PageSize)
                .ToListAsync(ct);

            return new PageResponse<User>
            {
                Items = items,
                PageIndex = pageRequest.PageIndex,
                PageSize = pageRequest.PageSize,
                TotalCount = totalCount
            };
        }

        private static IQueryable<User> ApplyOrderBy(IQueryable<User> query, PageRequest pageRequest)
        {
            if (string.IsNullOrWhiteSpace(pageRequest.OrderBy))
                return query.OrderBy(x => x.Id);

            string orderBy = pageRequest.OrderBy.Trim();

            return (orderBy, pageRequest.Desc) switch
            {
                (nameof(User.Email), false) => query.OrderBy(x => x.Email),
                (nameof(User.Email), true) => query.OrderByDescending(x => x.Email),

                (nameof(User.FirstName), false) => query.OrderBy(x => x.FirstName),
                (nameof(User.FirstName), true) => query.OrderByDescending(x => x.FirstName),

                (nameof(User.LastName), false) => query.OrderBy(x => x.LastName),
                (nameof(User.LastName), true) => query.OrderByDescending(x => x.LastName),

                (nameof(User.Id), false) => query.OrderBy(x => x.Id),
                (nameof(User.Id), true) => query.OrderByDescending(x => x.Id),

                _ when pageRequest.Desc => query.OrderByDescending(x => x.Id),
                _ => query.OrderBy(x => x.Id)
            };
        }
    }
}
