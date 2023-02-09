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

    public Task<DtResponce<TEntity>> LoadTable(DtParameters dtParameters)
    {
        //return _weatherForecastRepository.LoadTable(dtParameters);

        var searchBy = dtParameters.Search?.Value;

        // if we have an empty search then just order the results by Id ascending
        var orderCriteria = "Id";
        var orderAscendingDirection = true;

        if (dtParameters.Order != null)
        {
            // in this example we just default sort on the 1st column
            orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
            orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
        }

        Expression<Func<TEntity, bool>> searchByEx = default;

        if (!string.IsNullOrEmpty(searchBy))
        {
            searchByEx = r => r.Summary != null && r.Summary.ToUpper().Contains(searchBy.ToUpper());
        }

        return _repository.LoadTable(dtParameters.Draw, dtParameters.Start, dtParameters.Length, orderCriteria, orderAscendingDirection, searchByEx);
    }
}
