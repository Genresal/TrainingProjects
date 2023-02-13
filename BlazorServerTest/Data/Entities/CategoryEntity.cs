using BlazorServerTest.Data.Entities.Interfaces;

namespace BlazorServerTest.Data.Entities;
public class CategoryEntity : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }

    public ICollection<RecipeEntity> Recipes { get; set; }
}
