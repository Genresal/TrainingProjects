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
    }
}
