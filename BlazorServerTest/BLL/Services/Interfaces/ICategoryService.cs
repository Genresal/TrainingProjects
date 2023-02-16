using BlazorServerTest.BLL.Models;

namespace BlazorServerTest.BLL.Services.Interfaces
{
    public interface ICategoryService : IBaseService<CategoryModel>
    {
        Task CalculateRecipesQuantity();
    }
}
