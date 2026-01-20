using core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        IQueryable<TEntity> Query();

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken= default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity> DeleteAsync(TEntity entity, bool isSoftDelete = true, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool isSoftDelete = true, CancellationToken cancellationToken = default);
    }
}
