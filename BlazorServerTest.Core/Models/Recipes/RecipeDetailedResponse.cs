using BlazorServerTest.Core.Models.Categories;

namespace BlazorServerTest.Core.Models.Recipes;

public class RecipeDetailedResponse : RecipeResponse
{
    public IEnumerable<CategoryResponse>? Categories { get; set; } = new List<CategoryResponse>();
}
