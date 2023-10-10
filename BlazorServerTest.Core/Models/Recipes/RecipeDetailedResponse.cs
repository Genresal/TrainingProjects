using BlazorServerTest.Core.Models.Categories;
using BlazorServerTest.Core.Models.Ingredients;

namespace BlazorServerTest.Core.Models.Recipes;

public class RecipeDetailedResponse : RecipeResponse
{
    public IEnumerable<CategoryResponse>? Categories { get; set; } = new List<CategoryResponse>();

    public IEnumerable<RecipeIngredientResponse>? Ingredients { get; set; } = new List<RecipeIngredientResponse>();
}
