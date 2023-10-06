using AutoFixture;
using AutoMapper;
using BlazorServerTest.Core.Business;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories.Categories;
using BlazorServerTest.Core.Data.Repositories.Recipes;
using BlazorServerTest.Core.Models.Recipes;
using BlazorTestAppTests.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlazorTestAppTests;

public class RecipeManagerTests : IntegrationTestsBase
{
    private readonly Mock<ILogger<RecipeManager>> _loggerMock;
    private readonly CategoryRepository _categoryRepository;
    private readonly RecipeRepository _recipeRepository;
    private readonly RecipeManager _recipeManager;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper; // Add IMapper

    public RecipeManagerTests()
    {
        var configuration = new MapperConfiguration(cfg => { });

        _mapper = configuration.CreateMapper();

        _loggerMock = new Mock<ILogger<RecipeManager>>();
        _recipeRepository = new RecipeRepository(Context, _mapper);
        _categoryRepository = new CategoryRepository(Context, _mapper);
        _fixture = new Fixture();
        //_fixture.Customize<Recipe>(c => c.OmitAutoProperties());    // Avoiding circular references. No it create instance with nullable props
        _recipeManager = new RecipeManager(_loggerMock.Object, _mapper, _recipeRepository, _categoryRepository);
    }

    [Fact]
    public async Task Update_ValidInput_ReturnsUpdatedEntity( /*[Frozen] Recipe entity*/)
    {
        var categories = new List<Category>()
        {
            new Category {Name = "1"},
            new Category {Name = "2"},
        };

        await AddRangeToContext(categories);

        var entity = new Recipe()
        {
            Name = "Name",
            //           Categories = new List<Category>() { categories.First() }
        };
        await AddToContext(entity);

        var result =
            await _recipeManager.UpdateRecipeAsync(new UpdateRecipeRequest() { Id = entity.Id, CategoryIds = new List<long>() { 1, 2 } },
                CancellationToken.None);


        result.Categories.Should().HaveCountGreaterThan(1);
    }
}
