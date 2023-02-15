using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace InMemoryCachingLibrary
{
	public class CacheService : ICacheService
	{
		private readonly IMemoryCache _cache;
		private readonly CacheSettings _cacheSettings;
		private readonly ConcurrentDictionary<object, SemaphoreSlim> locks = new();
		public CacheService(IMemoryCache cache, IOptions<CacheSettings> cacheSettings)
		{
			_cache = cache;
			_cacheSettings = cacheSettings.Value;
		}

		public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, MemoryCacheEntryOptions options)
		{
			if (!_cache.TryGetValue(key, out T value))
			{
				SemaphoreSlim mylock = locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
				await mylock.WaitAsync();

				try
				{
					value = await factory();
					_cache.Set(key, value, options);
				}
				finally
				{
					mylock.Release();
				}
			}

			return value;
		}

		public Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, string optionName = default)
		{
			var options = GetMemoryCacheEntryOptions(typeof(T), optionName);

			return GetOrCreateAsync(key, factory, options);
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}

		private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(Type type, string optionName)
		{
			optionName ??= type.IsGenericType ? type.GetGenericArguments().First().Name : type.Name;

			if (!_cacheSettings.MemoryCacheDuration.TryGetValue(optionName, out var expirationTime))
			{
				throw new NotImplementedException($"The time for caching is not specified for the {optionName} entity.");
			}

			return new MemoryCacheEntryOptions()
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(expirationTime),
			};
		}
	}
}
