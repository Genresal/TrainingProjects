namespace BlazorServerTest.Core.Models.Common;

public abstract class SortedRequest : PagedRequest
{
    /// <summary>
    /// This is the field to parse to have the order by
    /// </summary>
    public string? OrderBy { get; set; }
}