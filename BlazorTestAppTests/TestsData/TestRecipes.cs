using BlazorServerTest.Core.Data.Entities;

namespace BlazorTestAppTests.TestsData;

public static class TestRecipes
{
    public static Recipe RecipeEntity => new()
    {
        //Id = 1,
        Name = "Test Recipe",
        Description = "This is a test recipe",
        PrepTime = 10,
        CookTime = 20,
        Servings = 4,
        Image = new byte[] { 0x12, 0x34, 0x56 },
        /*Ingredients = new List<IngredientEntity>
        {
            new IngredientEntity { Id = 1, Name = "Test Ingredient 1" },
            new IngredientEntity { Id = 2, Name = "Test Ingredient 2" }
        },
        Steps = new List<StepEntity>
        {
            new StepEntity { Id = 1, Description = "Step 1" },
            new StepEntity { Id = 2, Description = "Step 2" }
        },*/
        Categories = new List<Category>
        {
            new Category { Id = 1, },
            //new CategoryEntity { Id = 2, }
        }
    };

    public static List<Recipe> RecipeEntityCollection => new()
        {
            RecipeEntity,
        };
}

