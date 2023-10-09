using BlazorServerTest.Core.Models.Ingredients;

namespace BlazorServerTest.Core.Models.Recipes;

public class BaseRecipe
{
    public long Id { get; set; }

    public string? Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public double? AverageRating { get; set; }

    public List<RecipeIngredientRequest> Ingredients { get; set; }
}
