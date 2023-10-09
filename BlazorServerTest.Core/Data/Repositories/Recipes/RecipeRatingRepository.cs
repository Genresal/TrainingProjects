using AutoMapper;
using BlazorServerTest.Core.Data.Entities;

namespace BlazorServerTest.Core.Data.Repositories.Recipes;

public class RatingRepository : Repository<RecipeMark>
{
    public RatingRepository(ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }
}
