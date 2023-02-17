using BlazorServerTest.Core.Data.Entities.Interfaces;

namespace BlazorServerTest.Core.Data.Entities;
public class Step : IEntity
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
