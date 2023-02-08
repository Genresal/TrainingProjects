using BlazorServerTest.Data.Entities.Interfaces;
using BlazorServerTest.Data.Infrastructure;
using BlazorServerTest.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Data.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public Task<TEntity> Add(TEntity entity)
    {
        _dbSet.AddAsync(entity);
        return _context.SaveChangesAsync().ContinueWith(x => entity);
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await Get(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public Task<TEntity> Get(int id)
    {
        return _dbSet.FindAsync(id).AsTask();
    }

    public Task<List<TEntity>> GetAll()
    {
        return _dbSet.ToListAsync();
    }

    public virtual Task<TEntity> Update(TEntity entity)
    {
        _dbSet.Update(entity);

        return _context.SaveChangesAsync().ContinueWith(x => entity);
    }
}
