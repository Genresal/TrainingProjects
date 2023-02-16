using AutoMapper;
using BlazorServerTest.BLL.Models.Interfaces;
using BlazorServerTest.BLL.Services.Interfaces;
using BlazorServerTest.Data.Entities.Interfaces;
using BlazorServerTest.Data.Repositories.Interfaces;

namespace BlazorServerTest.BLL.Services;
public class BaseService<TModel, TEntity> : IBaseService<TModel> where TEntity : IEntity where TModel : IModel
{
    protected readonly IBaseRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TModel> Add(TModel model)
    {
        var entity = _mapper.Map<TEntity>(model);
        var result = await _repository.Add(entity);

        return _mapper.Map<TModel>(result);
    }

    public Task Delete(int id)
    {
        return _repository.Delete(id);
    }

    public async Task<TModel> Get(int id)
    {
        var result = await _repository.Get(id);

        return _mapper.Map<TModel>(result);
    }

    public virtual async Task<List<TModel>> GetAll()
    {
        var result = await _repository.GetAll();

        return _mapper.Map<List<TModel>>(result);
    }

    public async Task<TModel> Update(TModel model)
    {
        var entity = _mapper.Map<TEntity>(model);
        var result = await _repository.Update(entity);

        return _mapper.Map<TModel>(result);
    }
}
