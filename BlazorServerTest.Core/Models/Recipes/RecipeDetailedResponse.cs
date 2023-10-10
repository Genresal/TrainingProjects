using BlazorServerTest.Core.Models.Categories;
using BlazorServerTest.Core.Models.Ingredients;
using BlazorServerTest.Core.Models.Steps;

namespace BlazorServerTest.Core.Models.Recipes;

public class RecipeDetailedResponse : RecipeResponse
{
    public string? ImageUrl { get; set; }

    public IEnumerable<CategoryResponse>? Categories { get; set; } = new List<CategoryResponse>();

    public IEnumerable<RecipeIngredientResponse>? Ingredients { get; set; } = new List<RecipeIngredientResponse>();

    public IEnumerable<StepResponse>? Steps { get; set; } = new List<StepResponse>();
}
