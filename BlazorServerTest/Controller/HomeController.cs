﻿using BlazorServerTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerTest.Controller;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly WeatherForecastService _service;

    public HomeController(WeatherForecastService service)
    {
        _service = service;
    }

    [HttpPost("LoadTable")]
    public async Task<IActionResult> LoadTable([FromBody] DtParameters dtParameters)
    {
        var res = await _service.LoadTable(dtParameters);
        return Ok(res);
    }
}
