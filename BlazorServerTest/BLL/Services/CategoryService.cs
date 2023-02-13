using BlazorServerTest.BLL.Services.Interfaces;
using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Repositories.Interfaces;

namespace BlazorServerTest.BLL.Services
{
	public class CategoryService : BaseService<CategoryEntity>, ICategoryService
	{
		private IBaseRepository<CategoryEntity> _categoryRepository;
		private IRecipeRepository _recipeRepository;

		public CategoryService(IBaseRepository<CategoryEntity> categoryRepository, IRecipeRepository recipeRepository) : base(categoryRepository)
		{
			_categoryRepository = categoryRepository;
			_recipeRepository = recipeRepository;
		}

		public async Task CalculateRecipesQuantity()
		{
			var categories = await _categoryRepository.GetAll();

			foreach (var cat in categories)
			{
				var catRecipesCount = await _recipeRepository.Count(x => x.Categories.Contains(cat));

				if (cat.Quantity != catRecipesCount)
				{
					cat.Quantity = catRecipesCount;
					await _categoryRepository.Update(cat);
				}
			}
		}
	}
}
