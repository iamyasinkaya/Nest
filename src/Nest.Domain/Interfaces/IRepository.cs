using System.Linq.Expressions;

namespace Nest.Domain;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}
