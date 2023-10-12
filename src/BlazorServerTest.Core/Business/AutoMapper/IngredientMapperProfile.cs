using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Models.Ingredients;

namespace BlazorServerTest.Core.Business.AutoMapper;

public partial class AutoMapperProfile : Profile
{
    public void CreateIngredientMapperProfile()
    {
        CreateMap<Ingredient, BaseIngredient>();
        CreateMap<Ingredient, IngredientResponse>();
        CreateMap<Ingredient, RecipeIngredientResponse>();

        CreateMap<RecipeIngredient, RecipeIngredientResponse>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Ingredient.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ingredient.Name));
    }
}
