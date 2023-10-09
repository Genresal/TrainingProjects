using BlazorServerTest.Core.Models.Common;

namespace BlazorServerTest.Core.Models.Ingredients;

public class IngredientRequest : SortedRequest
{
    public string? Name { get; set; }
}
