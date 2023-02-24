using InMemoryCachingLibrary;
using InMemoryCachingLibrary.Services;
using MemoryCache.Testing.Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq.AutoMock;

namespace InMemoryLibraryTests.Infrastructure.Helpers
{
    internal static class AutoMockerExtensions
    {
        private const string ConfigFileName = "appsettings.json";
        private const string InvalidConfigFileName = "appsettings.Invalid.json";

        public static CacheService CreateValidInstanceOfMemoryCache(this AutoMocker mocker)
        {
            return CreateInstanceOfMemoryCache(mocker);
        }

        public static CacheService CreateInvalidInstanceOfMemoryCache(this AutoMocker mocker)
        {
            return CreateInstanceOfMemoryCache(mocker, InvalidConfigFileName);
        }

        private static CacheService CreateInstanceOfMemoryCache(this AutoMocker mocker, string configFileName = ConfigFileName)
        {
            var memoryCache = Create.MockedMemoryCache();
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(configFileName, false)
                    .Build();

            var options = Options.Create(configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>());
            var memoryCacheHelper = new CacheService(memoryCache, options);

            return memoryCacheHelper;
        }

        private static CacheService CreateInstanceOfMemoryCache1(this AutoMocker mocker, string configFileName = ConfigFileName)
        {
	        var memoryCache = Create.MockedMemoryCache();
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
