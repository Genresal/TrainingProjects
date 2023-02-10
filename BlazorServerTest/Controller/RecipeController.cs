using AutoMapper;
using BlazorServerTest.BLL.Services;
using BlazorServerTest.Data.Entities;
using BlazorServerTest.Validators;
using BlazorServerTest.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerTest.Controller;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly RecipeService _service;
    private readonly IMapper _mapper;
    private readonly ChangeRecipeValidator _validator;

    public RecipeController(RecipeService service, ChangeRecipeValidator validator, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpPost("LoadTable")]
    public async Task<IActionResult> LoadTable([FromBody] DtParameters dtParameters)
    {
        var res = await _service.LoadTable(dtParameters);
        /*
        if (res == null)
        {
            return NotFound();
        }*/

        return Ok(res);
    }

    [HttpPost]
    public async Task<RecipeEntity> Add([FromBody] ChangeRecipeViewModel viewModel)
    {
        await _validator.ValidateAndThrowAsync(viewModel);

        var model = _mapper.Map<RecipeEntity>(viewModel);

        return await _service.Add(model);
    }

    [HttpGet("{id}")]
    public async Task<RecipeEntity> Get(int id)
    {
        return await _service.Get(id);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}
