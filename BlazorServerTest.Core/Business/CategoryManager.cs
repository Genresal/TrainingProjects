using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories;
using BlazorServerTest.Core.Models.Categories;
using LinqKit;
using Microsoft.Extensions.Logging;

namespace BlazorServerTest.Core.Business;

public class CategoryManager
{
    private readonly ILogger<CategoryManager> _logger;
    private readonly IMapper _mapper;
    private readonly CategoryRepository _categoryRepository;
    private readonly RecipeRepository _recipeRepository;

    public CategoryManager(ILogger<CategoryManager> logger,
        IMapper mapper,
        CategoryRepository categoryRepository,
        RecipeRepository recipeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _recipeRepository = recipeRepository;
    }

    public async Task<CategoryPagedResponse> GetCategoriesAsync(CategoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to get categories list, page : {page}", request.Page);
        var predicate = PredicateBuilder.New<Category>(true);

        if (!string.IsNullOrEmpty(request.Name))
        {
            predicate = predicate.And(x => x.Name.ToLower().Contains(request.Name.ToLower()));
        }

        var result = await _categoryRepository.PagedFindAsync<CategoryResponse, object>(
            request.Page,
            request.PageSize,
            predicate,
            null,
            true,
            cancellationToken);


        foreach (var cat in result.Items)
        {
            var catRecipesCount = await _recipeRepository.CountByCategoryIdAsync(cat.Id);
            if (cat.Quantity != catRecipesCount)
            {
                cat.Quantity = catRecipesCount;
            }
        }

        return new CategoryPagedResponse
        {
            Items = result.Items,
            Page = result.Page,
            PageSize = result.PageSize,
            Total = result.Total
        };
    }

    public async Task<CategoryResponse> GetCategoryDetailAsync(long categoryId, CancellationToken cancellationToken = default)
    {
        var category = await GetCategoryAsync(categoryId, cancellationToken);

        var categoryDetails = _mapper.Map<CategoryResponse>(category);

        return categoryDetails;
    }

    public async Task<CategoryResponse?> UpdateCategoryAsync(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to update existing Category. Category ID : {ID}", request.Id);

        var category = await GetCategoryAsync(request.Id, cancellationToken);

        // For now we update only name and description
        category.Name = request.Name!;
        category.Description = request.Description;

        await _categoryRepository.UpdateAsync(category, cancellationToken: cancellationToken);

        _logger.LogInformation("The Category with ID : {ID} updated with name : {name} and description : {description}",
        category.Id, request.Name, request.Description);

        return await GetCategoryDetailAsync(request.Id, cancellationToken);
    }

    public async Task DeleteCategoryAsync(long categoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to delete the category. Space code: {ID}", categoryId);

        var category = await GetCategoryAsync(categoryId, cancellationToken);

        await _categoryRepository.DeleteAsync(category, cancellationToken: cancellationToken);
    }

    // Private methods

    private async Task<Category> GetCategoryAsync(long categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.FirstOrDefaultAsync<Category>(x => x.Id == categoryId, null, true, cancellationToken);

        if (category is null)
        {
            _logger.LogWarning("Unable to find category with code {categoryId}", categoryId);
            throw new Exception();
        }

        return category;
    }
}
