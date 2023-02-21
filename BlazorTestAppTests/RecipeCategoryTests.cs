using AutoFixture;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories;
using BlazorTestAppTests.Infrastructure;
using BlazorTestAppTests.TestsData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
				.Without(x => x.RecipeCategories)
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
