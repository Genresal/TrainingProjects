using BlazorServerTest.Data.Entities.Interfaces;
using BlazorServerTest.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BlazorTestAppTests.Infrastructure
{
    public class IntegrationTestsBase : IDisposable
    {
        protected AppDbContext Context { get; } = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb").Options);

        public async Task<int> AddToContext<T>(T entity) where T : class, IEntity
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