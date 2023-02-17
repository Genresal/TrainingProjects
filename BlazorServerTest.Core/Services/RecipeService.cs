using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Infrastructure;

namespace BlazorServerTest.Core.Services
{
    public class RecipeService : BaseService<Recipe>
    {
        public RecipeService(AppDbContext context) : base(context)
        {
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