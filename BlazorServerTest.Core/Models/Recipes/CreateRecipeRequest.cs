namespace BlazorServerTest.Core.Models.Recipes;

public class CreateRecipeRequest : BaseRecipe
{
    public List<long>? CategoryIds { get; set; } = new ();
}
