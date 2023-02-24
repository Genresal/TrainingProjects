using BlazorServerTest.Core.Data.Entities.Interfaces;
using BlazorServerTest.Core.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorServerTest.Core.Data.Repositories;
public class BaseRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual Task<TEntity> Add(TEntity entity)
    {
        _dbSet.AddAsync(entity);

        return _context.SaveChangesAsync().ContinueWith(x => entity);
    }

    public async Task Delete(int id)
    {
        var entity = await Get(id);

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<TEntity?> Get(int id)
    {
        return _dbSet.FindAsync(id).AsTask();
    }

    public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? predicate)
    {
        return _dbSet.Where(predicate)
            .ToListAsync();
    }

    public Task<int> Count(Expression<Func<TEntity, bool>>? predicate)
    {
        return _dbSet.CountAsync(predicate);
    }

    public virtual Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return query.ToListAsync();
    }

    public virtual Task<List<TEntity>> GetAll()
    {
        return _dbSet.ToListAsync();
    }

    public Task<TEntity> Update(TEntity entity)
    {/*
        _dbSet.Entry(entity).State = EntityState.Modified;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        */
        _dbSet.Update(entity);

        return _context.SaveChangesAsync().ContinueWith(x => entity);
    }
}
