using FluentAssertions;
using InMemoryLibraryTests.Infrastructure.Helpers;
using Moq;
using Moq.AutoMock;

namespace InMemoryLibraryTests
{
    public class Tests
    {
        [Test]
        public async Task GetOrCreateAsync_DataChangesBetwennCalls_ShouldReturnFromCache()
        {
            // Arrange
            var key = "testKey";
            var expectedValue = "testValue";
            var isFactoryCalled = false;
            var mocker = new AutoMocker(MockBehavior.Default, DefaultValue.Mock);

            Func<Task<string>> factory = () =>
            {
                isFactoryCalled = true;
                return Task.FromResult(isFactoryCalled ? expectedValue : string.Empty);
            };
            var memoryCache = mocker.CreateInstanceOfMemoryCache();

            // Act
            var firstCall = await memoryCache.GetOrCreateAsync(key, factory);
            var secondCall = await memoryCache.GetOrCreateAsync(key, factory);

            // Assert
            firstCall.Should().Be(expectedValue);
            secondCall.Should().Be(expectedValue);
        }
    }
}