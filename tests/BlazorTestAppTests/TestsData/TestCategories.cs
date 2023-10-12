using BlazorServerTest.Core.Data.Entities;

namespace BlazorTestAppTests.TestsData;

public static class TestCategories
{
    public static Category CategoryEntity => new()
    {
        Id = 1,
        Name = "Test Category",
        //Quantity = 10.5m,
        Unit = "lbs",
    };

    public static List<Category> RecipeEntityCollection => new()
        {
            CategoryEntity,
        };
}

