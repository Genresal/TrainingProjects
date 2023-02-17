using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using InMemoryCachingLibrary;
using InMemoryCachingLibrary.Services;
using InMemoryCachingLibrary.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.AutoMock;

namespace InMemoryLibraryTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var key = "testKey";
            var expectedValue = "testValue";
            var mocker = new AutoMocker(MockBehavior.Default, DefaultValue.Mock);
            mocker.Setup<IMemoryCache, bool>(x => x.TryGetValue(key, out expectedValue)).Returns(true);
            var memoryCache = mocker.CreateInstance<CacheService>();

            // Act
            var actualValue = await memoryCache.GetOrCreateAsync(key, () => Task.FromResult(expectedValue));

            // Assert
            actualValue.Should().Be(expectedValue);
        }
    }
}