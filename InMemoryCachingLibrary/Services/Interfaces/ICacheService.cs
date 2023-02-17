using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCachingLibrary.Services.Interfaces
{
    /// <summary>
    /// Memory cache service.
    /// </summary>
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, MemoryCacheEntryOptions options);
        /// <summary>
        /// Gets an item from cache, if item not exists, inserts a cache entry into the cache.
        /// </summary>
        /// <param name="key">An object identifying the requested entry.</param>
        /// <param name="factory">The method for retrieving data for caching.</param>
        /// <param name="optionName">Represents the cache options applied to an entry of the <see cref="IMemoryCache"/> instance.</param>
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, string optionName = default);
        /// <summary>
        /// Removes the item associated with the given key.
        /// </summary>
        /// <param name="key">An object identifying the requested entry.</param>
        void Remove(string key);
    }
}
