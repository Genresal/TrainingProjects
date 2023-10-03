using AutoMapper;
using BlazorServerTest.Core.Data.Entities;

namespace BlazorServerTest.Core.Data.Repositories;

public class CategoryRepository : Repository<Category>
{
    public CategoryRepository(ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }
}
