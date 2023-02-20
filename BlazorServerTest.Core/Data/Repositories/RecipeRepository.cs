using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Core.Data.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>
    {
        public RecipeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Recipe> Add(Recipe model, List<int>? categoryIds)
        {/*
            foreach (var id in categoryIds)
            {
                var category = new Category { Id = id };
                _context.Categories.Attach(category);
                model.Categories.Add(category);
            }
            */
            model.Categories = await _context.Categories.Where(x => categoryIds.Contains(x.Id)).ToListAsync();

            return await Add(model);
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