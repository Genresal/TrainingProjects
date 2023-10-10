using AutoMapper;
using BlazorServerTest.Core.Data.Contexts;
using BlazorServerTest.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Core.Data.Repositories.Recipes;

public class RecipeRepository : Repository<Recipe>
{
    public RecipeRepository(ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }

    public Task<Recipe?> GetFullDataByIdAsync(long id, CancellationToken cancellationToken)
    {
        return Context.Set<Recipe>()
            .Include(x => x.RecipeCategories)
            .ThenInclude(x => x.Category)
            .Include(x => x.Marks)
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
