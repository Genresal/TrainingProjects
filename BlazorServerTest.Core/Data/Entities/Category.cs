using BlazorServerTest.Core.Data.Entities.Interfaces;

namespace BlazorServerTest.Core.Data.Entities;
public class Category : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; }
}
