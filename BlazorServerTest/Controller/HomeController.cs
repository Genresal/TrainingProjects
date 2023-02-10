using BlazorServerTest.BLL.Services;
using BlazorServerTest.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerTest.Controller;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly RecipeService _service;

    public HomeController(RecipeService service)
    {
        _service = service;
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
