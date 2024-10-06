using Nest.Domain;

namespace Nest.Application;

public class BaseService<TEntity> where TEntity : class
{
    protected readonly IRepository<TEntity> _repository;

    public BaseService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _repository.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        await _repository.UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
