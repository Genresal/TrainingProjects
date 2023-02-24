using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Infrastructure;

namespace BlazorServerTest.Core.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        private readonly RecipeRepository _recipeRepository;

        public CategoryRepository(AppDbContext context, RecipeRepository recipeRepository) : base(context)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<List<Category>> CalculateRecipesQuantity()
        {
            var categories = await base.GetAll();

            foreach (var cat in categories)
            {
                var catRecipesCount = await _recipeRepository.Count(x => x.Categories.Contains(cat));

                if (cat.Quantity != catRecipesCount)
                {
                    cat.Quantity = catRecipesCount;
                    //await Update(cat);
                }
            }

            return categories;
        }

        public override Task<List<Category>> GetAll()
        {
            return CalculateRecipesQuantity();
        }
    }
}
