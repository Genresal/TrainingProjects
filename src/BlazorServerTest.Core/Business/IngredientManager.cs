using AutoMapper;
using BlazorServerTest.Core.Commons.Enums;
using BlazorServerTest.Core.Commons.Extensions;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories.Ingredients;
using BlazorServerTest.Core.Models.Common;
using BlazorServerTest.Core.Models.Ingredients;
using LinqKit;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BlazorServerTest.Core.Business;

public class IngredientManager
{
    private readonly ILogger<IngredientManager> _logger;
    private readonly IMapper _mapper;
    private readonly IngredientRepository _ingredientRepository;

    public IngredientManager(ILogger<IngredientManager> logger,
        IMapper mapper,
        IngredientRepository ingredientRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<IngredientPagedResponse> GetIngredientsAsync(IngredientRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to get ingredients list");
        var predicate = PredicateBuilder.New<Ingredient>(true);

        if (!string.IsNullOrEmpty(request.Name))
        {
            predicate = predicate.And(x => x.Name.ToLower().Contains(request.Name.ToLower()));
        }

        (var sortExpression, bool ascOrder) = GetOrderByExpression(request);

        var result = await _ingredientRepository.PagedFindAsync<IngredientResponse, object>(
            request.Page,
            request.PageSize,
            predicate,
            sortExpression,
            ascOrder,
            cancellationToken);

        return new IngredientPagedResponse
        {
            Items = result.Items,
            Page = result.Page,
            PageSize = result.PageSize,
            Total = result.Total
        };
    }

    public async Task<IngredientResponse> GetIngredientDetailAsync(long ingredientId, CancellationToken cancellationToken = default)
    {
        var ingredient = await GetIngredientAsync(ingredientId, cancellationToken);

        var ingredientDetails = _mapper.Map<IngredientResponse>(ingredient);

        return ingredientDetails;
    }

    public async Task<IngredientResponse?> CreateIngredientAsync(CreateIngredientRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to create new Ingredient");

        var ingredient = new Ingredient()
        {
            Name = request.Name
        };

        await _ingredientRepository.AddAsync(ingredient, cancellationToken: cancellationToken);

        _logger.LogInformation("New Ingredient created with Id {Id} and name {name}", ingredient.Id, ingredient.Name);

        return await GetIngredientDetailAsync(ingredient.Id, cancellationToken);
    }

    public async Task<IngredientResponse?> UpdateIngredientAsync(UpdateIngredientRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to update existing Ingredient. Ingredient ID : {ID}", request.Id);

        var ingredient = await GetIngredientAsync(request.Id, cancellationToken);

        ingredient.Name = request.Name!;

        await _ingredientRepository.UpdateAsync(ingredient, cancellationToken: cancellationToken);

        _logger.LogInformation("The Ingredient with ID : {ID} updated with name : {name}",
        ingredient.Id, request.Name);

        return await GetIngredientDetailAsync(request.Id, cancellationToken);
    }

    // Private methods

    private async Task<Ingredient> GetIngredientAsync(long ingredientId, CancellationToken cancellationToken)
    {
        var ingredient = await _ingredientRepository.FirstOrDefaultAsync<Ingredient>(x => x.Id == ingredientId, null, true, cancellationToken);

        if (ingredient is null)
        {
            _logger.LogWarning("Unable to find category with code {categoryId}", ingredientId);
            throw new Exception();
        }

        return ingredient;
    }

    private static (Expression<Func<Ingredient, object>>, bool) GetOrderByExpression(SortedRequest request)
    {
        var orderByClause = request.GetFirstOrderByClause();

        // default order
        Expression<Func<Ingredient, object>> sortExpression = x => x.Id;

        if (orderByClause.Key == nameof(Category.Name).ToLower())
        {
            sortExpression = x => x.Name;
        }

        var isAscending = orderByClause.Value == SortOrder.Ascending;

        return (sortExpression, isAscending);
    }
}
