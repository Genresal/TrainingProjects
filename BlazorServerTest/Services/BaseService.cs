using BlazorServerTest.Data.Entities.Interfaces;
using BlazorServerTest.Data.Repositories.Interfaces;
using BlazorServerTest.Services.Interfaces;
using System.Linq.Expressions;

namespace BlazorServerTest.Services;
public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : IEntity
{
    protected readonly IBaseRepository<TEntity> _repository;

    public BaseService(IBaseRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public Task<TEntity> Add(TEntity entity)
    {
        return _repository.Add(entity);
    }

    public Task<bool> Delete(int id)
    {
        return _repository.Delete(id);
    }

    public Task<TEntity> Get(int id)
    {
        return _repository.Get(id);
    }

    public Task<List<TEntity>> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<TEntity> Update(TEntity entity)
    {
        return _repository.Update(entity);
    }
}
