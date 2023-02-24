using InMemoryCachingLibrary;
using InMemoryCachingLibrary.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;

namespace InMemoryLibraryTests.Infrastructure.Extensions
{
	internal static class TestExtensions
	{
		private const string ConfigFileName = "appsettings.json";
		private const string InvalidConfigFileName = "appsettings.Invalid.json";

		public static CacheService CreateValidInstanceOfMemoryCache(Mock<IMemoryCache> memoryCacheMock = default)
		{
			return CreateInstanceOfMemoryCache(memoryCacheMock);
		}

		public static CacheService CreateInvalidInstanceOfMemoryCache(Mock<IMemoryCache> memoryCacheMock = default)
		{
			return CreateInstanceOfMemoryCache(memoryCacheMock, InvalidConfigFileName);
		}

		private static CacheService CreateInstanceOfMemoryCache(Mock<IMemoryCache> memoryCacheMock = default, string configFileName = ConfigFileName)
		{
			IMemoryCache memoryCache = memoryCacheMock is not null ? memoryCacheMock.Object : new MemoryCache(new MemoryCacheOptions());

			var configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile(configFileName, false)
					.Build();

			var options = Options.Create(configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>());
			var memoryCacheHelper = new CacheService(memoryCache, options);

			return memoryCacheHelper;
		}
	}
}
