using BlazorServerTest.Data.Entities;
using BlazorServerTest.ViewModels;

namespace BlazorServerTest.BLL.Services.Interfaces
{
	public interface ICategoryService : IBaseService<CategoryEntity>
	{
		Task CalculateRecipesQuantity();
		Task<List<CategoryViewModel>> GetAllViews();
	}
}
