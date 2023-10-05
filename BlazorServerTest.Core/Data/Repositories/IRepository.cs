using BlazorServerTest.Core.Data.Entities.Core;
using BlazorServerTest.Core.Models.Common;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace BlazorServerTest.Core.Data.Repositories;

public interface IRepository<T> where T : IEntity
{
    // -=======================-
    //  Persistency Management
    // -=======================-
    Task Complete();

    void Detach(T entity);

    void DetachRange(IEnumerable<T> entities);

    // -=======================-
    //  Retrieve
    // -=======================-
    Task<PagedResponse<T>> PagedFindAsync<P>(
        int page,
        int take,
        Expression<Func<T, bool>>? where = null,
        Expression<Func<T, P>>? orderBy = null,
        bool ascOrder = true,
        CancellationToken cancellationToken = default);

    Task<PagedResponse<TResult>> PagedFindAsync<TResult, P>(
        int page,
        int take,
        Expression<Func<T, bool>>? where = null,
        Expression<Func<T, P>>? orderBy = null,
        bool ascOrder = true,
        CancellationToken cancellationToken = default);

    Task<List<T>> FindAsync<P>(
        Expression<Func<T, bool>> where,
        Expression<Func<T, P>> orderBy,
        bool ascOrder = true,
        CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsync<P>(
        Expression<Func<T, bool>> where,
        Expression<Func<T, P>> orderBy,
        bool ascOrder = true,
        CancellationToken cancellationToken = default);

    Task<T?> GetItemByIdAsync(long id, CancellationToken cancellationToken = default);

    // -=======================-
    //  Create
    // -=======================-
    Task<T> AddAsync(T entity, bool complete = true, CancellationToken cancellationToken = default);

    Task<List<T>> AddRangeAsync(List<T> entities, bool complete = true, CancellationToken cancellationToken = default);

    // -=======================-
    //  Update
    // -=======================-
    Task<T> UpdateAsync(T entity, bool complete = true, CancellationToken cancellationToken = default);

    Task<List<T>> UpdateRangeAsync(List<T> entities, bool complete = true, CancellationToken cancellationToken = default);

    // -=======================-
    //  Delete
    // -=======================-
    Task DeleteAsync(T entity, bool complete = true, CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(IEnumerable<T> entities, bool complete = true, CancellationToken cancellationToken = default);

    // -=======================-
    //  Transactions
    // -=======================-

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitTransactionAsync(IDbContextTransaction transaction);

    Task RollBackTransactionAsync(IDbContextTransaction transaction);
}