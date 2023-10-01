using AutoFixture;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories;
using BlazorTestAppTests.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlazorTestAppTests
{
    public class RecipeCategoriesTests : IntegrationTestsBase
    {
        private readonly Mock<ILogger<RecipeRepository>> _loggerMock;
        private readonly CategoryRepository _categoryRepository;
        private readonly RecipeRepository _recipeRepository;
        private readonly Fixture _fixture;

        public RecipeCategoriesTests()
        {
            _loggerMock = new Mock<ILogger<RecipeRepository>>();
            _recipeRepository = new RecipeRepository(Context);
            _categoryRepository = new CategoryRepository(Context, _recipeRepository);
            _fixture = new Fixture();
            //_fixture.Customize<Recipe>(c => c.OmitAutoProperties());    // Avoiding circular references. No it create instance with nullable props

        }

        [Fact]
        //[AutoDomainDataAttribute]
        //[Theory, AutoData]
        //[Theory, AutoDataWithOmitOnRecursion]
        public async Task Test23(/*[Frozen] Recipe entity*/)
        {
            //Arrange
            var entity = _fixture.Build<Recipe>()
                .Without(x => x.Categories)
                .Without(x => x.Steps)
                .Without(x => x.Ingredients)
                .Create();
            await AddToContext(entity);

            //Act
            var result = await _recipeRepository.Get(entity.Id);

            //Assert
            result.Should().BeEquivalentTo(entity);
        }
    }
}
