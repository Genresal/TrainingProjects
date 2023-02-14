using BlazorServerTest.BLL.Services.Interfaces;
using BlazorServerTest.Data.Entities.Interfaces;
using BlazorServerTest.Data.Repositories.Interfaces;

namespace BlazorServerTest.BLL.Services;
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

    public Task Delete(int id)
    {
        return _repository.Delete(id);
    }

    public Task<TEntity> Get(int id)
    {
        return _repository.Get(id);
    }

    virtual public Task<List<TEntity>> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<TEntity> Update(TEntity entity)
    {
        return _repository.Update(entity);
    }
}
