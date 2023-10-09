using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Models.Recipes;

namespace BlazorServerTest.Core.Business.AutoMapper;

public partial class AutoMapperProfile : Profile
{
    public void CreateRecipeMapperProfile()
    {
        CreateMap<Recipe, BaseRecipe>();
        CreateMap<Recipe, RecipeResponse>();
        CreateMap<Recipe, RecipeDetailedResponse>()
            .ForMember(d => d.Categories, o => o.MapFrom(s => s.RecipeCategories.Select(x => x.Category)))
            //.ForMember(d => d.Ingredients, o => o.MapFrom(s => s.RecipeIngredients.Select(x => x.Ingredient)))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating));
    }
}
