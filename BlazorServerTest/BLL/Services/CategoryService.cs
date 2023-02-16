using AutoMapper;
using BlazorServerTest.BLL.Models;
using BlazorServerTest.BLL.Services.Interfaces;
using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Repositories.Interfaces;
using BlazorServerTest.ViewModels;
using InMemoryCachingLibrary;

namespace BlazorServerTest.BLL.Services
{
	public class CategoryService : BaseService<CategoryModel, CategoryEntity>, ICategoryService
	{
		private readonly IBaseRepository<CategoryEntity> _categoryRepository;
		private readonly IRecipeRepository _recipeRepository;
		private readonly IMapper _mapper;
		private readonly ICacheService _cacheService;

		public CategoryService(IBaseRepository<CategoryEntity> categoryRepository, IRecipeRepository recipeRepository, IMapper mapper, ICacheService cacheService) : base(categoryRepository, mapper)
		{
			_categoryRepository = categoryRepository;
			_recipeRepository = recipeRepository;
			_mapper = mapper;
			_cacheService = cacheService;
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

		public override Task<List<CategoryModel>> GetAll()
		{
			var key = nameof(CategoryModel);

			return _cacheService.GetOrCreateAsync(key, () => base.GetAll());
		}
    }
}
