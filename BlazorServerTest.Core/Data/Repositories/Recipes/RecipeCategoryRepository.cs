using AutoMapper;
using BlazorServerTest.Core.Data.Entities;

namespace BlazorServerTest.Core.Data.Repositories.Recipes;

public class RecipeCategoryRepository : Repository<RecipeCategory>
{
    public RecipeCategoryRepository(ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }
}
