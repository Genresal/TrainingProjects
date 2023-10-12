using AutoMapper;
using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Models.Steps;

namespace BlazorServerTest.Core.Business.AutoMapper;

public partial class AutoMapperProfile : Profile
{
    public void CreateStepMapperProfile()
    {
        CreateMap<Step, BaseStep>();
        CreateMap<Step, StepResponse>();
    }
}
