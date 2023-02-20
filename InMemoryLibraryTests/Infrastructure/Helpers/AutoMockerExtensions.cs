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
        public static CacheService CreateInstanceOfMemoryCache(this AutoMocker mocker)
        {
            var memoryCache = Create.MockedMemoryCache();
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false)
                    .Build();

            var options = Options.Create(configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>());
            var memoryCacheHelper = new CacheService(memoryCache, options);

            return memoryCacheHelper;
        }
    }
}
