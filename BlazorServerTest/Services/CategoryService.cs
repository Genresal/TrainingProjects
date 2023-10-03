using BlazorServerTest.Core.Data.Repositories;
using BlazorServerTest.Core.Models.Categories;
using Mapster;
using MudBlazor;

namespace BlazorServerTest.Services;

public class CategoryService
{
    private readonly CategoryRepository _repository;
    public CategoryService(CategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<TableData<CategoryResponse>> LoadTable(TableState state, string searchString)
    {
        // rawData = await _repository.GetAllAsync();
        var rawData = new List<CategoryResponse>();
        var data = rawData.Adapt<IEnumerable<CategoryResponse>>();

        //move search to db
        if (!string.IsNullOrEmpty(searchString))
        {
            data = data.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToArray();
        }

        var totalItems = data.Count();
        switch (state.SortLabel)
        {
            case "id_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Id);
                break;
            case "name_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Name);
                break;
            case "quantity_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Quantity);
                break;
        }

        var pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
        var mappedData = pagedData.Adapt<List<CategoryResponse>>();
        return new TableData<CategoryResponse>() { TotalItems = totalItems, Items = mappedData };
    }
}
