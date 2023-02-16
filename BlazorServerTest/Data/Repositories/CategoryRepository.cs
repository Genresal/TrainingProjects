using BlazorServerTest.Data.Entities;
using BlazorServerTest.Data.Infrastructure;
using BlazorServerTest.Data.Repositories.Interfaces;

namespace BlazorServerTest.Data.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
