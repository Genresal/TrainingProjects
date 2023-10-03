using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Core.Data.Repositories
{
    public class RecipeRepository : Repository<Recipe>
    {
        public RecipeRepository(ApplicationDbContext context,
            IMapper mapper) : base(context, mapper)
        {
        }

        public Task<int> CountByCategoryIdAsync(long categoryId)
        {
            return Context.Set<Recipe>()
                .Where(recipe => recipe.Categories.Any(category => category.Id == categoryId))
                .CountAsync();
        }

        public async Task<Recipe> AddAsync(Recipe entity)
        {
            foreach (var category in entity.Categories)
            {
                Context.Attach(category);
            }

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();

            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<Recipe> UpdateAsync(Recipe entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            var cats = Context.RecipeCategories.Where(x => x.RecipeId == entity.Id);
            Context.RecipeCategories.RemoveRange(cats);
            var newCats = entity.Categories.Select(x => new RecipeCategory() { RecipeId = entity.Id, CategoryId = x.Id });
            Context.RecipeCategories.AddRange(newCats);

            await Context.SaveChangesAsync();

            Context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public Task<Recipe> AddDefaultAsync()
        {
            return AddAsync(new Recipe
            {
                Name = "DefaultRecipe"
            });
        }
    }
}