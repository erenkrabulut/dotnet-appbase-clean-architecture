using core.Application.Abstractions.Repositories;
using core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace core.Persistence.Repositories
{
    public class EFRepository<TEntity, TId, TContext> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TContext : DbContext
    {

        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _set;

        public EFRepository(TContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            return _set;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _set.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreatedAt = DateTime.UtcNow;

            await _set.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (TEntity entity in entities)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            await _set.AddRangeAsync(entities, cancellationToken);

            return entities;
        }

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _set.Update(entity);

            return Task.FromResult(entity);
        }
        public Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }

            _set.UpdateRange(entities);

            return Task.FromResult(entities);
        }

        public Task<TEntity> DeleteAsync(TEntity entity, bool isSoftDelete, CancellationToken cancellationToken = default)
        {
            if (isSoftDelete)
            {
                if (entity.DeletedAt == null)
                {
                    entity.DeletedAt = DateTime.UtcNow;
                    _set.Update(entity);
                }
            }
            else
            {
                _set.Remove(entity);
            }

            return Task.FromResult(entity);
        }

        public Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool isSoftDelete = true, CancellationToken cancellationToken = default)
        {
            if (isSoftDelete)
            {
                foreach (var entity in entities)
                {
                    if (entity.DeletedAt == null)
                        entity.DeletedAt = DateTime.UtcNow;
                }

                _set.UpdateRange(entities);
            }
            else
            {
                _set.RemoveRange(entities);
            }

            return Task.FromResult(entities);
        }

    }
}
