using BlazorServerTest.Core.Data.Entities.Interfaces;

namespace BlazorServerTest.Core.Data.Entities;
public class Ingredient : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }


    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
