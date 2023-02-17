using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Infrastructure;
using InMemoryCachingLibrary;

namespace BlazorServerTest.Core.Services
{
    public class CategoryService : BaseService<Category>
    {
        private readonly RecipeService _recipeService;
        private readonly ICacheService _cacheService;

        public CategoryService(AppDbContext context, RecipeService recipeService, ICacheService cacheService) : base(context)
        {
            _recipeService = recipeService;
            _cacheService = cacheService;
        }

        public async Task CalculateRecipesQuantity()
        {
            var categories = await base.GetAll();

            foreach (var cat in categories)
            {
                var catRecipesCount = await _recipeService.Count(x => x.Categories.Contains(cat));

                if (cat.Quantity != catRecipesCount)
                {
                    cat.Quantity = catRecipesCount;
                    await Update(cat);
                }
            }
        }

        public override Task<List<Category>> GetAll()
        {
            var key = nameof(Category);

            return _cacheService.GetOrCreateAsync(key, () => base.GetAll());
        }
    }
}
