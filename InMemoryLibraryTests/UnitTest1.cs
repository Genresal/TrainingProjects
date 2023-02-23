using FluentAssertions;
using InMemoryLibraryTests.Infrastructure.Helpers;
using Moq;
using Moq.AutoMock;

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
            var mocker = new AutoMocker(MockBehavior.Default, DefaultValue.Mock);

            Func<Task<string>> factory = () =>
            {
                isFactoryCalled = true;
                return Task.FromResult(isFactoryCalled ? expectedValue : string.Empty);
            };
            var memoryCache = mocker.CreateValidInstanceOfMemoryCache();

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
            var mocker = new AutoMocker(MockBehavior.Default, DefaultValue.Mock);

            Func<Task<string>> factory = () => Task.FromResult(expectedValue);

            var memoryCache = mocker.CreateInvalidInstanceOfMemoryCache();

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
            var mocker = new AutoMocker(MockBehavior.Default, DefaultValue.Mock);

            Func<Task<int>> factory = () => Task.FromResult(expectedValue);

            var memoryCache = mocker.CreateValidInstanceOfMemoryCache();

            // Act
            Action action = () => memoryCache.GetOrCreateAsync(key, factory);

            // Assert
            action.Should().Throw<NotImplementedException>();
        }
    }
}