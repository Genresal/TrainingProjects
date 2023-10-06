using BlazorServerTest.Core.Models.Common;
using System.Web;

namespace BlazorServerTest.Core.Models.Recipes;

public class RecipeRequest : SortedRequest
{
    public string? Name { get; set; }

    public List<long>? CategoryIds { get; set; }

    // For Minimal API 
    public static bool TryParse(string queryString, out RecipeRequest request)
    {
        request = new RecipeRequest();

        var queryStringValues = HttpUtility.ParseQueryString(queryString);

        if (queryStringValues["page"] != null)
        {
            request.Name = queryStringValues["page"];
        }

        if (queryStringValues["pageSize"] != null)
        {
            request.Name = queryStringValues["pageSize"];
        }

        if (queryStringValues["name"] != null)
        {
            request.Name = queryStringValues["name"];
        }

        if (queryStringValues["categoryIds"] != null)
        {
            request.CategoryIds = queryStringValues["categoryIds"].Split(',').Select(long.Parse).ToList();
        }

        return true;
    }
}
