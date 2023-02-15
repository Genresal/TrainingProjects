using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCachingLibrary
{
	public interface ICacheService
	{
		Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, MemoryCacheEntryOptions options);
		Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, string optionName = default);
		void Remove(string key);
	}
}
