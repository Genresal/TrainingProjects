using AutoMapper;
using BlazorServerTest.BLL.Models;
using BlazorServerTest.Data.Entities;

namespace BlazorServerTest.BLL.Profiles;

public class BllMappingProfile : Profile
{
	public BllMappingProfile()
	{
		CreateMap<RecipeModel, RecipeEntity>().ReverseMap();

		CreateMap<CategoryModel, CategoryEntity>().ReverseMap();
	}
}
