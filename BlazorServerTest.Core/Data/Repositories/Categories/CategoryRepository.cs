using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Core.Data.Repositories.Categories;

public class CategoryRepository : Repository<Category>
{
    public CategoryRepository(ApplicationDbContext context,
        IMapper mapper) : base(context, mapper)
    {
    }

    public Task<Category?> GetFullDataByIdAsync(long id, CancellationToken cancellationToken)
    {
        return Context.Set<Category>()
            .Include(x => x.RecipeCategories)
            .ThenInclude(x => x.Recipe)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<Category>> GetByIdListAsync(List<long> ids, CancellationToken cancellationToken)
    {
        return Context.Set<Category>()
            .Where(e => ids.Contains(e.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
