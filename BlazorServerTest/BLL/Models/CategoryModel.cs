using BlazorServerTest.BLL.Models.Interfaces;

namespace BlazorServerTest.BLL.Models;
public class CategoryModel : IModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal? Quantity { get; set; }
    public bool IsSelected { get; set; }
}
