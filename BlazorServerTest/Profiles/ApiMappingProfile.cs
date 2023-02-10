using AutoMapper;
using BlazorServerTest.Data.Entities;
using BlazorServerTest.ViewModels;

namespace BlazorServerTest.Profiles;
public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<ChangeRecipeViewModel, RecipeEntity>();
    }
}
