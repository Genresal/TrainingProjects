using BlazorServerTest.Core.Data.Entities.Core;

namespace BlazorServerTest.Core.Data.Entities;

public class RecipeMark : Entity
{
    public int Rating { get; set; }

    public long RecipeId { get; set; }

    public Recipe Recipe { get; set; }
}
