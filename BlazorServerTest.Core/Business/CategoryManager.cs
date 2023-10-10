using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories.Categories;
using BlazorServerTest.Core.Enums;
using BlazorServerTest.Core.Extensions;
using BlazorServerTest.Core.Models.Categories;
using BlazorServerTest.Core.Models.Common;
using LinqKit;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BlazorServerTest.Core.Business;

public class CategoryManager
{
    private readonly ILogger<CategoryManager> _logger;
    private readonly IMapper _mapper;
    private readonly CategoryRepository _categoryRepository;

    public CategoryManager(ILogger<CategoryManager> logger,
        IMapper mapper,
        CategoryRepository categoryRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryPagedResponse> GetCategoriesAsync(CategoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to get categories list, page : {page}", request.Page);
        var predicate = PredicateBuilder.New<Category>(true);

        if (!string.IsNullOrEmpty(request.Name))
        {
            predicate = predicate.And(x => x.Name.ToLower().Contains(request.Name.ToLower()));
        }

        (var sortExpression, bool ascOrder) = GetOrderByExpression(request);

        var result = await _categoryRepository.PagedFindAsync<CategoryResponse, object>(
            request.Page,
            request.PageSize,
            predicate,
            sortExpression,
            ascOrder,
            cancellationToken);

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
        var category = await _categoryRepository.GetFullDataByIdAsync(categoryId, cancellationToken);

        var categoryDetails = _mapper.Map<CategoryResponse>(category);

        return categoryDetails;
    }

    public async Task<CategoryResponse?> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to create new Category");

        var category = new Category()
        {
            Name = request.Name, //Guid.NewGuid().ToString(),
            Description = request.Description,
        };

        await _categoryRepository.AddAsync(category, cancellationToken: cancellationToken);

        _logger.LogInformation("New Category created with Id {Id} and name {name}", category.Id, category.Name);

        return await GetCategoryDetailAsync(category.Id, cancellationToken);
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

    private static (Expression<Func<Category, object>>, bool) GetOrderByExpression(SortedRequest request)
    {
        var orderByClause = request.GetFirstOrderByClause();

        // default order
        Expression<Func<Category, object>> sortExpression = x => x.Id;

        if (orderByClause.Key == nameof(Category.Name).ToLower())
        {
            sortExpression = x => x.Name;
        }
        else if (orderByClause.Key == nameof(Category.Description).ToLower())
        {
            sortExpression = x => x.Description;
        }
        else if (orderByClause.Key == nameof(Category.Quantity).ToLower())
        {
            sortExpression = x => x.Quantity;
        }

        var isAscending = orderByClause.Value == SortOrder.Ascending;

        return (sortExpression, isAscending);
    }
}
