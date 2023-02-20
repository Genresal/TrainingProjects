namespace BlazorServerTestApi.VIewModels;
public class ChangeRecipeViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<int>? CategoryIds { get; set; }
}
