using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Infrastructure;

namespace BlazorServerTest.Core.Data.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>
    {
        public RecipeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Recipe> Add(Recipe model, List<int>? categoryIds)
        {
            await Add(model);

            var categoryRecipes = categoryIds.Select(x => new RecipeCategory() { CategoryId = x, RecipeId = model.Id });
            model.RecipeCategories.AddRange(categoryRecipes);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<IEnumerable<Recipe>> GetForecastAsync(string? search)
        {
            var data = (await GetAll()).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                return data.Where(x => x.Name is not null && x.Name.ToUpper().Contains(search.ToUpper()));
            }

            return data;
        }

        public Task<Recipe> AddDefault()
        {
            return Add(new Recipe
            {
                Name = "DefaultRecipe"
            });
        }
    }
}