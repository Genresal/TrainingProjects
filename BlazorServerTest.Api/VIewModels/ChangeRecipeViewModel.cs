using BlazorServerTest.Core.Models.Categories;

namespace BlazorServerTestApi.VIewModels;
public class ChangeRecipeViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<CategoryResponse>? Categories { get; set; }
}
