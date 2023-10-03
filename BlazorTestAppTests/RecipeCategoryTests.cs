using AutoFixture;
using AutoMapper;
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
        private readonly IMapper _mapper; // Add IMapper

        public RecipeCategoriesTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
            });

            _mapper = configuration.CreateMapper();

            _loggerMock = new Mock<ILogger<RecipeRepository>>();
            _recipeRepository = new RecipeRepository(Context, _mapper);
            _categoryRepository = new CategoryRepository(Context, _mapper);
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
            var result = await _recipeRepository.FirstOrDefaultAsync<Recipe>(x => x.Id == entity.Id, null, true, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(entity);
        }
    }
}
