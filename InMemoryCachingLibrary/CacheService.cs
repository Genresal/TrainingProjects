using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCachingLibrary
{
	public class CacheService : ICacheService
	{
		private readonly IMemoryCache _cache;
		public CacheService(IMemoryCache cache)
		{
			_cache = cache;
		}

		public async Task<T> GetOrCreateAsync<T>(string key, TimeSpan expiration, Func<Task<T>> factory)
		{
			if (!_cache.TryGetValue(key, out T value))
			{
				value = await factory();
				var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(expiration);
				_cache.Set(key, value, cacheEntryOptions);
			}

			return value;
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}
	}
}
