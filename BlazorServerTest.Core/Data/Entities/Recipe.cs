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
    public double? AverageRating { get; set; }

    public virtual ICollection<Step> Steps { get; set; }
    public virtual ICollection<RecipeMark> Marks { get; set; }
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    public List<RecipeCategory>? RecipeCategories { get; set; }

    public double? CalculateRating()
    {
        if (Marks == null || !Marks.Any())
            return null;

        return Marks.Average(x => x.Rating);
    }
}
