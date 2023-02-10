using BlazorServerTest.Data.Entities.Interfaces;

namespace BlazorServerTest.Data.Entities;
public class StepEntity : IEntity
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }

    public int RecipeId { get; set; }
    public RecipeEntity Recipe { get; set; }
}
