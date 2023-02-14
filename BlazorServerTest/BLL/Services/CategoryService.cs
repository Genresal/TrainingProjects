using AutoMapper;
using BlazorServerTest.BLL.Services.Interfaces;
using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Repositories.Interfaces;
using BlazorServerTest.ViewModels;
using InMemoryCachingLibrary;

namespace BlazorServerTest.BLL.Services
{
	public class CategoryService : BaseService<CategoryEntity>, ICategoryService
	{
		private readonly IBaseRepository<CategoryEntity> _categoryRepository;
		private readonly IRecipeRepository _recipeRepository;
		private readonly IMapper _mapper;
		private readonly ICacheService _cacheService;

		public CategoryService(IBaseRepository<CategoryEntity> categoryRepository, IRecipeRepository recipeRepository, IMapper mapper, ICacheService cacheService) : base(categoryRepository)
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

		public override Task<List<CategoryEntity>> GetAll()
		{
			return _cacheService.GetOrCreateAsync("test", TimeSpan.FromSeconds(30), () => _repository.GetAll());
		}

		public async Task<List<CategoryViewModel>> GetAllViews()
		{
			return _mapper.Map<List<CategoryViewModel>>(await _repository.GetAll());
		}
	}
}
