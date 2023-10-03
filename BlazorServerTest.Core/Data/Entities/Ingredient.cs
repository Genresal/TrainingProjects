using BlazorServerTest.Core.Data.Entities.Core;

namespace BlazorServerTest.Core.Data.Entities;
public class Ingredient : Entity
{
    public string? Name { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }


    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
