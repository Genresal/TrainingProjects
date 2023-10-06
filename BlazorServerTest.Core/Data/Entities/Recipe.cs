using BlazorServerTest.Core.Data.Entities.Core;

namespace BlazorServerTest.Core.Data.Entities;
public class Recipe : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? PrepTime { get; set; }
    public int? CookTime { get; set; }
    public int? Servings { get; set; }
    public byte[]? Image { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; }
    public virtual ICollection<Step> Steps { get; set; }
    public List<RecipeCategory>? RecipeCategories { get; set; }
}
