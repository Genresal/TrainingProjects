using BlazorServerTest.Core.Data.Models;

namespace BlazorServerTest.Core.Models.Categories;

public class CategoryRequest : SortedRequest
{
    public string? Name { get; set; }
}
