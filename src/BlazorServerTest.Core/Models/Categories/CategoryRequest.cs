using BlazorServerTest.Core.Models.Common;

namespace BlazorServerTest.Core.Models.Categories;

public class CategoryRequest : SortedRequest
{
    public string? Name { get; set; }
}
