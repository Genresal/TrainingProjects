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

        public override async Task<Recipe> Add(Recipe entity)
        {
            foreach (var category in entity.Categories)
            {
                _context.Attach(category);
            }

            await _context.AddAsync(entity);
            /*
            var categoryRecipes = entity.Categories.Select(x => new RecipeCategory() { CategoryId = x.Id, RecipeId = entity.Id });
            _context.RecipeCategories.AddRange(categoryRecipes);
            */
            await _context.SaveChangesAsync();

            _dbSet.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public override async Task<Recipe> Update(Recipe entity)
        {
            _dbSet.Entry(entity).State = EntityState.Modified;

            var cats = _context.RecipeCategories.Where(x => x.RecipeId == entity.Id);
            _context.RecipeCategories.RemoveRange(cats);
            var newCats = entity.Categories.Select(x => new RecipeCategory() { RecipeId = entity.Id, CategoryId = x.Id });
            _context.RecipeCategories.AddRange(newCats);

            await _context.SaveChangesAsync();

            _dbSet.Entry(entity).State = EntityState.Detached;

            return entity;
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