using BlazorServerTest.Core.Data.Repositories;
using BlazorServerTest.Core.Models;
using Mapster;
using MudBlazor;

namespace BlazorServerTest.Services;

public class RecipeViewService
{
    private readonly RecipeRepository _repository;
    public RecipeViewService(RecipeRepository repository)
    {
        _repository = repository;
    }

    public async Task<TableData<RecipeViewModel>> LoadTable(TableState state, string searchString)
    {
        var rawData = await _repository.GetAll(includeProperties: x => x.Categories);
        var data = rawData.Adapt<IEnumerable<RecipeViewModel>>();

        //move search to db
        if (!string.IsNullOrEmpty(searchString))
        {
            data = data.Where(x => x.Description.ToLower().Contains(searchString.ToLower())).ToArray();
        }


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
        }

        var pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
        var mappedData = pagedData.Adapt<List<RecipeViewModel>>();
        return new TableData<RecipeViewModel>() { TotalItems = totalItems, Items = mappedData };
    }
}
