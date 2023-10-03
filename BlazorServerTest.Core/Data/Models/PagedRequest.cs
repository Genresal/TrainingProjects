namespace BlazorServerTest.Core.Data.Models;

public abstract class PagedRequest
{
    /// <summary>
    /// Current Page.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; set; }
}