using BlazorServerTest.Core.Data.Entities.Core;

namespace BlazorServerTest.Core.Data.Entities;
public class Ingredient : Entity
{
    public string? Name { get; set; }
    public string? Unit { get; set; }

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
}
