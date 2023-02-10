using BlazorServerTest.BLL.Services;
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
}
