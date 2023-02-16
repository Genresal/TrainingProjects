using BlazorServerTest.BLL.Models.Interfaces;

namespace BlazorServerTest.BLL.Models
{
	public class RecipeModel : IModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int? PrepTime { get; set; }
		public int? CookTime { get; set; }

		public List<CategoryModel> Categories { get; set; }
	}
}
