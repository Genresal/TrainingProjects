using BlazorServerTest.Core.Data.Entities.Core;

namespace BlazorServerTest.Core.Data.Entities;
public class RecipeIngredient : Entity
{
    public double Quantity { get; set; }


    public long IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    public long RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
