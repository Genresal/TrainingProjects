using FluentAssertions;
using InMemoryLibraryTests.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace InMemoryLibraryTests
{
	public class Tests
	{
		[Test]
		public async Task GetOrCreateAsync_DataChangesBetweenCalls_ShouldReturnFromCache()
		{
			// Arrange
			var key = "testString";
			var expectedValue = "testValue";
			var isFactoryCalled = false;

			Func<Task<string>> factory = () =>
			{
				isFactoryCalled = true;
				return Task.FromResult(isFactoryCalled ? expectedValue : string.Empty);
			};
			var memoryCache = TestExtensions.CreateValidInstanceOfMemoryCache();

			// Act
			var firstCall = await memoryCache.GetOrCreateAsync(key, factory);
			var secondCall = await memoryCache.GetOrCreateAsync(key, factory);

			// Assert
			firstCall.Should().Be(expectedValue);
			secondCall.Should().Be(expectedValue);
		}

		[Test]
		public async Task GetOrCreateAsync_AppSettingsNotValid_ShouldThrowNotImplementedException()
		{
			// Arrange
			var key = "testKey";
			var expectedValue = "testValue";

			Func<Task<string>> factory = () => Task.FromResult(expectedValue);

			var memoryCache = TestExtensions.CreateInvalidInstanceOfMemoryCache();

			// Act
			Action action = () => memoryCache.GetOrCreateAsync(key, factory);

			// Assert
			action.Should().Throw<NotImplementedException>();
		}

		[Test]
		public async Task GetOrCreateAsync_DataStorageTimeNotSet_ShouldThrowNotImplementedException()
		{
			// Arrange
			var key = "testInt";
			var expectedValue = 1;

			Func<Task<int>> factory = () => Task.FromResult(expectedValue);

			var memoryCache = TestExtensions.CreateValidInstanceOfMemoryCache();

			// Act
			Action action = () => memoryCache.GetOrCreateAsync(key, factory);

			// Assert
			action.Should().Throw<NotImplementedException>();
		}

		[Test]
		public void GetOrCreateAsync_DataChangesBetweenCalls_1()
		{
			// Arrange
			var key = "testString1";

			var memoryCacheMock = new Mock<IMemoryCache>();
			memoryCacheMock.Setup(mc => mc.Remove(It.IsAny<object>())).Verifiable();

			var memoryCacheService = TestExtensions.CreateValidInstanceOfMemoryCache(memoryCacheMock);

			// Act
			memoryCacheService.Remove(key);

			// Assert
			memoryCacheMock.Verify(x => x.Remove(key), Times.Once);
		}
	}
}