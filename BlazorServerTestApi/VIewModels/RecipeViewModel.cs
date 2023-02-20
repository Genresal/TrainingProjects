namespace BlazorServerTestApi.VIewModels;

public class RecipeViewModel
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public int? PrepTime { get; set; }
	public int? CookTime { get; set; }
	public int? Servings { get; set; }
}
