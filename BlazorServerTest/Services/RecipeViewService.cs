using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Services;
using MudBlazor;

namespace BlazorServerTest.Services;

public class RecipeViewService
{
    private readonly RecipeService _service;
    public RecipeViewService(RecipeService service)
    {
        _service = service;
    }

    public async Task<TableData<Recipe>> LoadTable(TableState state)
    {
        var data = await _service.GetForecastAsync("");
        var totalItems = data.Count();
        switch (state.SortLabel)
        {
            case "nr_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Id);
                break;
            case "sign_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Description);
                break;
            case "name_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Name);
                break;
            case "position_field":
                data = data.OrderByDirection(state.SortDirection, o => o.CookTime);
                break;
            case "mass_field":
                data = data.OrderByDirection(state.SortDirection, o => o.PrepTime);
                break;
        }

        var pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
        return new TableData<Recipe>() { TotalItems = totalItems, Items = pagedData };
    }
}
