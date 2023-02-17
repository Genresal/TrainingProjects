using BlazorTestAppTests.Infrastructure;

namespace BlazorTestAppTests
{
    public class RecipeCategoriesTests : IntegrationTestsBase
    {/*
        private readonly Mock<ILogger<IRecipeRepository>> _loggerMock;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeCategoriesTests()
        {
            _loggerMock = new Mock<ILogger<IRecipeRepository>>();
            _categoryRepository = new CategoryRepository(Context);
            _recipeRepository = new RecipeRepository(Context, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_Entity_ReturnsEntity()
        {
            await AddToContext(TestCategories.CategoryEntity);
            //Arrange
            var entity = TestRecipes.RecipeEntity;

            //Act
            var trackedList = Context.ChangeTracker.Entries();
            var actualResult = await _recipeRepository.Add(entity);
            var categories = await _categoryRepository.GetAll();

            foreach (var cat in categories)
            {
                var catRecipesCount = await _recipeRepository.Count(x => x.Categories.Contains(cat));

                trackedList = Context.ChangeTracker.Entries();

                if (cat.Quantity != catRecipesCount)
                {
                    cat.Quantity = catRecipesCount;
                    await _categoryRepository.Update(cat);
                }
            }

            var lastRecipe = Context.Recipes.Include(x => x.Categories).Last();

            //Assert
            actualResult.Should().BeEquivalentTo(entity);
            lastRecipe.Should().BeEquivalentTo(entity);
            lastRecipe.Categories.Should().Contain(categories);
            categories.Last().Quantity.Should().Be(1);
        }

        [Fact]
        public async Task Test2()
        {
            await AddToContext(TestCategories.CategoryEntity);
            //Arrange
            var entity = TestRecipes.RecipeEntity;

            //Act
            var actualResult = await _recipeRepository.Add(entity);
            var categories = await _categoryRepository.GetAll();

            var lastRecipe = Context.Recipes.Include(x => x.Categories).Last();

            //Assert
            actualResult.Should().BeEquivalentTo(entity);
            lastRecipe.Should().BeEquivalentTo(entity, opt => opt.Excluding(x => x.Categories));
            categories.Last().Quantity.Should().Be(1);


        }*/
    }
}
