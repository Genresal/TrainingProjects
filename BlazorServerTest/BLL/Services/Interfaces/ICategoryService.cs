using BlazorServerTest.Data.Entities;

namespace BlazorServerTest.BLL.Services.Interfaces
{
	public interface ICategoryService : IBaseService<CategoryEntity>
	{
		Task CalculateRecipesQuantity();
	}
}
