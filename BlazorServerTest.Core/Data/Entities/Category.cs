using BlazorServerTest.Core.Data.Entities.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorServerTest.Core.Data.Entities;
public class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Unit { get; set; }

    [NotMapped]
    public int? Quantity => CalculateQuantity();


    public virtual ICollection<Recipe>? Recipes { get; set; }

    private int? CalculateQuantity()
    {
        if (Recipes == null)
            return null;

        return Recipes.Count;
    }
}
