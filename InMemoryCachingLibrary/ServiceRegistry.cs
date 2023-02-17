﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InMemoryCachingLibrary
{
	public static class ServiceRegistry
	{
		public static void AddInMemoryCachingSevice(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
			services.AddSingleton<ICacheService, CacheService>();
			services.AddMemoryCache();
		}
	}
}