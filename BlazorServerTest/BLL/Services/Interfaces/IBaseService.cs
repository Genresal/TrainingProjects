using BlazorServerTest.Data.Entities.Interfaces;

namespace BlazorServerTest.BLL.Services.Interfaces
{
    public interface IBaseService<TModel> where TModel : IEntity
    {
        Task<TModel> Get(int id);
        Task<List<TModel>> GetAll();
        Task<TModel> Add(TModel entity);
        Task<TModel> Update(TModel entity);
        Task<bool> Delete(int id);
    }
}
