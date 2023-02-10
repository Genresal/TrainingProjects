using BlazorServerTest.Data.Entities.Interfaces;

namespace BlazorServerTest.Data.Entities;
public class IngredientEntity : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }


    public int RecipeId { get; set; }
    public RecipeEntity Recipe { get; set; }
}
