using AutoMapper;
using BlazorServerTest.Core.Data.Entities;

namespace BlazorServerTest.Core.Data.Repositories.Ingredients;

public class IngredientRepository : Repository<Ingredient>
{
    public IngredientRepository(ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }
}
