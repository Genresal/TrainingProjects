using BlazorServerTest.Core.Data.Contexts;
using BlazorServerTest.Core.Data.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace BlazorTestAppTests.Infrastructure
{
    public class IntegrationTestsBase : IDisposable
    {
        protected ApplicationDbContext Context { get; } = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb").Options);

        public async Task<long> AddToContext<T>(T entity) where T : class, IEntity
        {
            var dbSet = Context.Set<T>();
            await dbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            dbSet.Entry(entity).State = EntityState.Detached;

            return entity.Id;
        }

        public async Task AddRangeToContext<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            var dbSet = Context.Set<T>();
            await dbSet.AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}