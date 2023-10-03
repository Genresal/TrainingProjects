using AutoMapper;

namespace BlazorServerTest.Core.Business.AutoMapper;

public partial class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateCategoryMapperProfile();
    }
}
