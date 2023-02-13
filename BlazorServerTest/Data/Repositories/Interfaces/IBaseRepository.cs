using BlazorServerTest.Data.Entities.Interfaces;
using System.Linq.Expressions;

namespace BlazorServerTest.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> Get(int id);
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
        Task<int> Count(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(int id);

        Task<DtResponce<TEntity>> LoadTable(int draw,
            int start,
            int length,
            string orderCriteria,
            bool orderAscendingDirection,
            Expression<Func<TEntity, bool>>? searchBy = null);
    }
}
