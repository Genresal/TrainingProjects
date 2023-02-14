namespace InMemoryCachingLibrary
{
	public interface ICacheService
	{
		Task<T> GetOrCreateAsync<T>(string key, TimeSpan expiration, Func<Task<T>> factory);
		void Remove(string key);
	}
}
