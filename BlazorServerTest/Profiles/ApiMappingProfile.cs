using AutoMapper;
using BlazorServerTest.BLL.Models;
using BlazorServerTest.Data.Entities;
using BlazorServerTest.ViewModels;

namespace BlazorServerTest.Profiles;
public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<ChangeRecipeViewModel, RecipeEntity>();
        CreateMap<RecipeEntity, RecipeViewModel>();

        CreateMap<int, CategoryModel>().ForMember(dest => dest.Id, opts => opts.MapFrom(src => src));
        CreateMap<CategoryModel, CategoryViewModel>().ReverseMap();
    }
}
