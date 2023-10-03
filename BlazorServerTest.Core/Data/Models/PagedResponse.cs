namespace BlazorServerTest.Core.Data.Models;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;

    public int Total { get; set; }
}