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
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, bool isSoftDelete = true, CancellationToken cancellationToken = default);
    }
}
