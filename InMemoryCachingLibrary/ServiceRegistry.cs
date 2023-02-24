using InMemoryCachingLibrary.Services;
using InMemoryCachingLibrary.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace InMemoryCachingLibrary
{
	[ExcludeFromCodeCoverage]
	public static class ServiceRegistry
	{
		public static void AddInMemoryCachingService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMemoryCache();
			services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
			services.AddSingleton<ICacheService, CacheService>();

		}
	}
}
