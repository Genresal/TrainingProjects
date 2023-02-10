using BlazorServerTest.Data.Entities.Interfaces;

namespace BlazorServerTest.Data.Entities;
public class RecipeEntity : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int Servings { get; set; }
    public byte[] Image { get; set; }
    public ICollection<IngredientEntity> Ingredients { get; set; }
    public ICollection<StepEntity> Steps { get; set; }
    public ICollection<CategoryEntity> Categories { get; set; }
}
