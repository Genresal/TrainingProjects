using AutoMapper;
using BlazorServerTest.Core.Data.Contexts;
using BlazorServerTest.Core.Data.Entities;

namespace BlazorServerTest.Core.Data.Repositories.Recipes;

public class RecipeIngredientRepository : Repository<RecipeIngredient>
{
    public RecipeIngredientRepository(ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }
}
