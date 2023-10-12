using BlazorServerTest.Core.Commons.Enums;
using BlazorServerTest.Core.Models.Common;

namespace BlazorServerTest.Core.Commons.Extensions;

public static class SortExtensions
{
    public static List<KeyValuePair<string, SortOrder>> GetOrderByClauses(this SortedRequest request)
    {
        var fieldsAndOrders = new List<KeyValuePair<string, SortOrder>>();

        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            var orderParams = request.OrderBy.Trim().Split(',');

            foreach (var param in orderParams)
            {
                var values = param.Split(":");

                fieldsAndOrders.Add(
                    new KeyValuePair<string, SortOrder>(values[0].ToLower(), values.Length == 2 && values[1].ToLower().Equals("descending") ? SortOrder.Descending : SortOrder.Ascending)
                );
            }
        }
        return fieldsAndOrders;
    }

    public static KeyValuePair<string, SortOrder> GetFirstOrderByClause(this SortedRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.OrderBy))
        {
            var firstOrderParams = request.OrderBy.Trim().Split(',').First();
            var values = firstOrderParams.Split(":");

            return new KeyValuePair<string, SortOrder>(values[0].ToLower(), values.Length == 2 && values[1].ToLower().Equals("descending") ? SortOrder.Descending : SortOrder.Ascending);
        }

        return default;
    }
}
