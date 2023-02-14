namespace BlazorServerTest.ViewModels;

public class ChangeRecipeViewModel
{
	public string Name { get; set; }
	public string? Description { get; set; }
	public List<int> Categories { get; set; } = new();

	public int CookTime { get; set; }
}
