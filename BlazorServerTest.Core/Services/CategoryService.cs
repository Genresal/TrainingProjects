using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Infrastructure;

namespace BlazorServerTest.Core.Services
{
    public class CategoryService : BaseService<Category>
    {
        private readonly RecipeService _recipeService;

        public CategoryService(AppDbContext context, RecipeService recipeService) : base(context)
        {
            _recipeService = recipeService;
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
    }
}
