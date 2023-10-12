using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Models.Categories;

namespace BlazorServerTest.Core.Business.AutoMapper;

public partial class AutoMapperProfile : Profile
{
    public void CreateCategoryMapperProfile()
    {
        CreateMap<Category, BaseCategory>();
        CreateMap<Category, CategoryResponse>();
    }
}
