using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories.Recipes;
using BlazorServerTest.Core.Models.Recipes;
using Microsoft.Extensions.Logging;

namespace BlazorServerTest.Core.Business;

public class RecipeRatingManager
{
    private readonly ILogger<RecipeManager> _logger;
    private readonly IMapper _mapper;
    private readonly RecipeRepository _recipeRepository;
    private readonly RatingRepository _recipeRatingRepository;

    public RecipeRatingManager(ILogger<RecipeManager> logger,
        IMapper mapper,
        RecipeRepository recipeRepository,
        RatingRepository recipeRatingRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _recipeRepository = recipeRepository;
        _recipeRatingRepository = recipeRatingRepository;
    }

    public async Task AddRatingAsync(RatingRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to date recipe");

        var rating = new RecipeMark()
        {
            RecipeId = request.RecipeId,
            Rating = request.Rating,
        };

        var recipe = await _recipeRepository.GetFullDataByIdAsync(request.RecipeId, cancellationToken);

        recipe.Marks.Add(rating);
        recipe.AverageRating = recipe.CalculateRating();

        await _recipeRepository.UpdateAsync(recipe, cancellationToken: cancellationToken);

        _logger.LogInformation("The recipe has been rated");
    }
}
