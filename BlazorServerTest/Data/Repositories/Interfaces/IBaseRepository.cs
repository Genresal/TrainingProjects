using BlazorServerTest.Data.Entities.Interfaces;

namespace BlazorServerTest.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> Get(int id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(int id);
    }
}
