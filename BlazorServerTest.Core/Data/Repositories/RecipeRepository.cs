using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Infrastructure;

namespace BlazorServerTest.Core.Data.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>
    {
        public RecipeRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Recipe> Add(Recipe model)
        {
            foreach (var category in model.Categories)
            {
                _context.Attach(category);
            }

            await _context.AddAsync(model);
            /*
            var categoryRecipes = model.Categories.Select(x => new RecipeCategory() { CategoryId = x.Id, RecipeId = model.Id });
            _context.RecipeCategories.AddRange(categoryRecipes);
            */
            await _context.SaveChangesAsync();

            return model;
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