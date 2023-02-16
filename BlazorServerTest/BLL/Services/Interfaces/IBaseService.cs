using BlazorServerTest.BLL.Models.Interfaces;

namespace BlazorServerTest.BLL.Services.Interfaces
{
    public interface IBaseService<TModel> where TModel : IModel
    {
        Task<TModel> Get(int id);
        Task<List<TModel>> GetAll();
        Task<TModel> Add(TModel entity);
        Task<TModel> Update(TModel entity);
        Task Delete(int id);
    }
}
