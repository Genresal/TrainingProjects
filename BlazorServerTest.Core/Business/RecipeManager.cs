using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories.Categories;
using BlazorServerTest.Core.Data.Repositories.Recipes;
using BlazorServerTest.Core.Enums;
using BlazorServerTest.Core.Extensions;
using BlazorServerTest.Core.Models.Common;
using BlazorServerTest.Core.Models.Recipes;
using LinqKit;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BlazorServerTest.Core.Business;

public class RecipeManager
{
    private readonly ILogger<RecipeManager> _logger;
    private readonly IMapper _mapper;
    private readonly RecipeRepository _recipeRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly RecipeCategoryRepository _recipeCategoryRepository;

    public RecipeManager(ILogger<RecipeManager> logger,
        IMapper mapper,
        RecipeRepository recipeRepository,
        CategoryRepository categoryRepository,
        RecipeCategoryRepository recipeCategoryRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _recipeRepository = recipeRepository;
        _categoryRepository = categoryRepository;
        _recipeCategoryRepository = recipeCategoryRepository;
    }

    public async Task<RecipePagedResponse> GetRecipesAsync(RecipeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to get recipes list, page : {page}", request.Page);
        var predicate = PredicateBuilder.New<Recipe>(true);

        if (!string.IsNullOrEmpty(request.Name))
        {
            predicate = predicate.And(x => x.Name.ToLower().Contains(request.Name.ToLower()));
        }

        if (request.CategoryIds is not null && request.CategoryIds.Any())
        {
            predicate = predicate.And(x => x.RecipeCategories.Any(x => request.CategoryIds.Contains(x.CategoryId)));
        }

        if (request.StartDateTime != null)
        {
            predicate = predicate.And(x => x.Created >= request.StartDateTime);
        }

        if (request.EndDateTime != null)
        {
            predicate = predicate.And(x => x.Created <= request.EndDateTime);
        }

        (var sortExpression, bool ascOrder) = GetOrderByExpression(request);

        var result = await _recipeRepository.PagedFindAsync<RecipeResponse, object>(
            request.Page,
            request.PageSize,
            predicate,
            sortExpression,
            ascOrder,
            cancellationToken);

        return new RecipePagedResponse
        {
            Items = result.Items,
            Page = result.Page,
            PageSize = result.PageSize,
            Total = result.Total
        };
    }

    public async Task<RecipeDetailedResponse> GetRecipeDetailAsync(long recipeId, CancellationToken cancellationToken = default)
    {
        var recipe = await _recipeRepository.GetFullDataByIdAsync(recipeId, cancellationToken);

        var recipeDetails = _mapper.Map<RecipeDetailedResponse>(recipe);

        return recipeDetails;
    }

    public async Task<RecipeResponse?> CreateRecipeAsync(CreateRecipeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to create new Category");

        var recipe = new Recipe()
        {
            Name = request.Name, //Guid.NewGuid().ToString(),
            Description = request.Description,
        };

        await _recipeRepository.AddAsync(recipe, cancellationToken: cancellationToken);

        var categories = await _categoryRepository.GetByIdListAsync(request.CategoryIds, cancellationToken);
        foreach (Category category in categories)
        {
            await _recipeCategoryRepository.AddAsync(new RecipeCategory() { RecipeId = recipe.Id, CategoryId = category.Id }, cancellationToken: cancellationToken);
        }

        _logger.LogInformation("New Category created with Id {Id} and name {name}", recipe.Id, recipe.Name);

        return await GetRecipeDetailAsync(recipe.Id, cancellationToken);
    }

    public async Task<RecipeDetailedResponse?> UpdateRecipeAsync(UpdateRecipeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to update existing Category. Category ID : {ID}", request.Id);

        var recipe = await _recipeRepository.GetFullDataByIdAsync(request.Id, cancellationToken);

        // For now we update only name and description
        recipe.Name = request.Name!;
        recipe.Description = request.Description;

        // Update the categories of the existing entitн
        var categories = await _categoryRepository.GetByIdListAsync(request.CategoryIds, cancellationToken);
        foreach (var recipeCategory in recipe.RecipeCategories.ToList())
        {
            if (!request.CategoryIds.Contains(recipeCategory.CategoryId))
                await _recipeCategoryRepository.DeleteAsync(recipeCategory, cancellationToken: cancellationToken);
        }

        foreach (Category category in categories)
        {
            if (!recipe.RecipeCategories.Any(x => x.CategoryId == category.Id))
                await _recipeCategoryRepository.AddAsync(new RecipeCategory() { RecipeId = recipe.Id, CategoryId = category.Id }, cancellationToken: cancellationToken);
        }

        await _recipeRepository.UpdateAsync(recipe, cancellationToken: cancellationToken);

        _logger.LogInformation("The Category with ID : {ID} updated with name : {name} and description : {description}",
        recipe.Id, request.Name, request.Description);

        return await GetRecipeDetailAsync(request.Id, cancellationToken);
    }

    public async Task DeleteRecipeAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to delete the Recipe. Recipe id: {ID}", id);

        var category = await GetRecipeAsync(id, cancellationToken);

        await _recipeRepository.DeleteAsync(category, cancellationToken: cancellationToken);
    }

    // Private methods

    private async Task<Recipe> GetRecipeAsync(long id, CancellationToken cancellationToken)
    {
        var category = await _recipeRepository.FirstOrDefaultAsync<Recipe>(x => x.Id == id, null, true, cancellationToken);

        if (category is null)
        {
            _logger.LogWarning("Unable to find Recipe with code {categoryId}", id);
            throw new Exception();
        }

        return category;
    }

    private static (Expression<Func<Recipe, object>>, bool) GetOrderByExpression(SortedRequest request)
    {
        var orderByClause = request.GetFirstOrderByClause();

        // default order
        Expression<Func<Recipe, object>> sortExpression = x => x.Id;

        if (orderByClause.Key == nameof(Recipe.Name).ToLower())
        {
            sortExpression = x => x.Name;
        }
        else if (orderByClause.Key == nameof(Recipe.Description).ToLower())
        {
            sortExpression = x => x.Description;
        }

        var isAscending = orderByClause.Value == SortOrder.Ascending;

        return (sortExpression, isAscending);
    }
}
