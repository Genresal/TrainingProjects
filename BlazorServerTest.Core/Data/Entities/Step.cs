using BlazorServerTest.Core.Data.Entities.Core;

namespace BlazorServerTest.Core.Data.Entities;
public class Step : Entity
{
    public string? Description { get; set; }
    public int? Order { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
