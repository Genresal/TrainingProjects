using Microsoft.Extensions.DependencyInjection;

namespace InMemoryCachingLibrary
{
	public static class ServiceRegistry
	{
		public static void AddInMemoryCachingSevice(this IServiceCollection services)
		{
			services.AddSingleton<ICacheService, CacheService>();
		}
	}
}
