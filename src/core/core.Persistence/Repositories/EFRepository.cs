using core.Application.Abstractions.Repositories;
using core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace core.Persistence.Repositories
{
    public class EFRepository<TEntity, TId, TContext> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TContext: DbContext
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
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach(TEntity entity in entities)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            await _set.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _set.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
        public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (TEntity entity in entities)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }

            _set.UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, bool isSoftDelete, CancellationToken cancellationToken = default)
        {
            if (isSoftDelete)
            {
                if(entity.DeletedAt == null)
                {
                    entity.DeletedAt = DateTime.UtcNow;
                    _set.Update(entity);
                }
            }
            else
            {
                _set.Remove(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool isSoftDelete = true, CancellationToken cancellationToken = default)
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

            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }

    }
}
