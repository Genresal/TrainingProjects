using BlazorServerTest.Data.Entities;

namespace BlazorTestAppTests.TestsData;

public static class TestCategories
{
    public static CategoryEntity CategoryEntity => new()
    {
        Id = 1,
        Name = "Test Category",
        Quantity = 10.5m,
        Unit = "lbs",
        Recipes = new List<RecipeEntity>(),
    };

    public static List<CategoryEntity> RecipeEntityCollection => new()
        {
            CategoryEntity,
        };
}

