using BlazorServerTest.Core.Models.Ingredients;

namespace BlazorServerTest.Core.Models.Recipes;

public class CreateRecipeRequest : BaseRecipe
{
    public List<long>? CategoryIds { get; set; } = new();

    public List<RecipeIngredientRequest> RecipeIngredients { get; set; }
}
