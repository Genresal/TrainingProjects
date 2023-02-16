using BlazorServerTest.Data.Entities.Interfaces;
using BlazorServerTest.Data.Exceptions;
using BlazorServerTest.Data.Extensions;
using BlazorServerTest.Data.Infrastructure;
using BlazorServerTest.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<TEntity> Get(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
        {
            throw new DataNotFoundException(
                $"Entity {typeof(TEntity).FullName} with Id {id} was not found in the database");
        }

        return entity;
    }

    public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? predicate)
    {
        return _dbSet.AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public Task<int> Count(Expression<Func<TEntity, bool>>? predicate)
    {
        return _dbSet.CountAsync(predicate);
    }

    public Task<List<TEntity>> GetAll()
    {
        return _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual Task<TEntity> Update(TEntity entity)
    {
        _dbSet.Update(entity);

        return _context.SaveChangesAsync().ContinueWith(x => entity);
    }

    public async Task<DtResponce<TEntity>> LoadTable(int draw,
        int start,
        int length,
        string orderCriteria,
        bool orderAscendingDirection,
        Expression<Func<TEntity, bool>>? searchBy = null)
    {
        var result = _dbSet.AsQueryable();

        if (searchBy != null)
        {
            result = result.Where(searchBy);
        }

        result = orderAscendingDirection
            ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc)
            : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

        var filteredResultsCount = await result.CountAsync();
        var totalResultsCount = await _dbSet.CountAsync();

        return new DtResponce<TEntity>()
        {
            Draw = draw,
            RecordsTotal = totalResultsCount,
            RecordsFiltered = filteredResultsCount,
            Data = await result
                .Skip(start)
                .Take(length)
                .ToListAsync()
        };
    }
}
