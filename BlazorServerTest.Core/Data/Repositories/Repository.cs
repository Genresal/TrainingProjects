using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorServerTest.Core.Data.Entities.Core;
using BlazorServerTest.Core.Data.Models;
using BlazorServerTest.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;

namespace BlazorServerTest.Core.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    public const int CHUCK_SIZE = 300;

    protected readonly ApplicationDbContext Context;
    protected readonly IMapper Mapper;

    public Repository(ApplicationDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    // -=======================-
    //  Persistency Management
    // -=======================-
    public async Task Complete()
    {
        await Context.SaveChangesAsync();
    }

    public void Detach(T entity)
    {
        Context.Entry(entity).State = EntityState.Detached;
    }

    public void DetachRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }
    }

    // -=======================-
    //  Retrieve
    // -=======================-
    public async Task<PagedResponse<T>> PagedFindAsync<P>(
        int page,
        int take,
        Expression<Func<T, bool>>? where = null,
        Expression<Func<T, P>>? orderBy = null,
        bool ascOrder = true, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<T>().AsQueryable();

        if (where != null)
        {
            query = query.Where(where);
        }

        if (orderBy != null)
        {
            query = ascOrder
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);
        }

        var results = await query.Skip((page - 1) * take).Take(take).ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);

        return new PagedResponse<T> { Page = page, PageSize = take, Items = results, Total = count };
    }

    public async Task<PagedResponse<TResult>> PagedFindAsync<TResult, P>(
             int page,
             int take,
             Expression<Func<T, bool>>? where = null,
             Expression<Func<T, P>>? orderBy = null,
             bool ascOrder = true,
     CancellationToken cancellationToken = default)
    {
        var query = Context.Set<T>().AsQueryable();

        if (where != null)
        {
            query = query.Where(where);
        }

        if (orderBy != null)
        {
            query = ascOrder
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);
        }

        var results = await query.Skip((page - 1) * take).Take(take).ProjectTo<TResult>(Mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var count = await query.CountAsync(cancellationToken);

        return new PagedResponse<TResult> { Page = page, PageSize = take, Items = results, Total = count };
    }

    public async Task<PagedResponse<TResult>> PagedFindAsync<TResult, P>(
     int page,
     int take,
     Expression<Func<T, bool>>? where = null,
     List<SortOrder<T, P>>? orderBys = null,
     CancellationToken cancellationToken = default)
    {
        var query = Context.Set<T>().AsQueryable();

        if (where != null)
        {
            query = query.Where(where);
        }

        if (orderBys is { Count: > 0 })
        {
            var firstItem = orderBys[0];

            // Apply the first order by method
            query = firstItem.AscSort
                ? query.OrderBy(firstItem.Order!)
                : query.OrderByDescending(firstItem.Order!);

            for (int i = 1; i < orderBys.Count; i++)
            {
                var item = orderBys[i];

                query = item.AscSort
                   ? ((IOrderedQueryable<T>)query).ThenBy(item.Order!)
                   : ((IOrderedQueryable<T>)query).ThenByDescending(item.Order!);
            }
        }

        var results = await query.Skip((page - 1) * take).Take(take).ProjectTo<TResult>(Mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var count = await query.CountAsync(cancellationToken);

        return new PagedResponse<TResult> { Page = page, PageSize = take, Items = results, Total = count };
    }

    public async Task<List<T>> FindAsync<P>(
        Expression<Func<T, bool>> where,
        Expression<Func<T, P>> orderBy,
        bool ascOrder = true, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<T>().AsQueryable();

        if (where != null)
        {
            query = query.Where(where);
        }

        if (orderBy != null)
        {
            query = ascOrder
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);
        }

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync<P>(
        Expression<Func<T, bool>> where,
        Expression<Func<T, P>> orderBy,
        bool ascOrder = true, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<T>().AsQueryable();

        if (where != null)
        {
            query = query.Where(where);
        }

        if (orderBy != null)
        {
            query = ascOrder
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);
        }

        return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<T?> GetItemByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<T>().AsQueryable();

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    // -=======================-
    //  Create
    // -=======================-
    public async Task<T> AddAsync(
        T entity,
        bool complete = true, CancellationToken cancellationToken = default)
    {
        // force the timestamp
        entity.Created = DateTime.UtcNow;

        await Context.Set<T>().AddAsync(entity, cancellationToken);
        if (complete)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }

    public async Task<List<T>> AddRangeAsync(List<T> entities,
        bool complete = true, CancellationToken cancellationToken = default)
    {
        var chunkEntityList = entities.ChunkBy(CHUCK_SIZE);

        foreach (var chunkEntities in chunkEntityList)
        {
            foreach (var entity in chunkEntities)
            {
                entity.Created = DateTime.UtcNow;
                await Context.Set<T>().AddAsync(entity, cancellationToken);
            }

            if (complete)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        return entities;
    }

    // -=======================-
    //  Update
    // -=======================-
    public async Task<T> UpdateAsync(
        T entity,
        bool complete = true, CancellationToken cancellationToken = default)
    {
        entity.Updated = DateTime.UtcNow;

        Context.Entry(entity).State = EntityState.Modified;
        if (complete)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        return entity;
    }

    public async Task<List<T>> UpdateRangeAsync(
        List<T> entities,
        bool complete = true, CancellationToken cancellationToken = default)
    {
        var chunkEntityList = entities.ChunkBy(CHUCK_SIZE);
        foreach (var chunkEntities in chunkEntityList)
        {
            chunkEntities.ForEach(entity =>
            {
                entity.Updated = DateTime.UtcNow;
                Context.Entry(entity).State = EntityState.Modified;
            });
            if (complete)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        return entities;
    }

    // -=======================-
    //  Delete
    // -=======================-
    public async Task DeleteAsync(
        T entity,
        bool complete = true, CancellationToken cancellationToken = default)
    {
        Context.Remove(entity);
        if (complete)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteRangeAsync(
        IEnumerable<T> entities,
        bool complete = true, CancellationToken cancellationToken = default)
    {
        var chunkEntityList = entities.ChunkBy(CHUCK_SIZE);
        foreach (var chunkEntities in chunkEntityList)
        {
            Context.RemoveRange(chunkEntities);
            if (complete)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
        }
    }

    // -=======================-
    //  Transactions
    // -=======================-

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await Context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        await transaction.CommitAsync();
    }

    public async Task RollBackTransactionAsync(IDbContextTransaction transaction)
    {
        await transaction.RollbackAsync();
    }


    // -=======================-
    //  Others
    // -=======================-
}