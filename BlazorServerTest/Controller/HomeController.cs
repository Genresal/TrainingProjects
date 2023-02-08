using BlazorServerTest.Services;
using jQueryDatatableServerSideNetCore.Models.AuxiliaryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTest.Controller;
[IgnoreAntiforgeryToken]
[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly WeatherForecastService _service;

    public HomeController(WeatherForecastService service)
    {
        _service = service;
    }

    //[HttpGet]
    [HttpPost("LoadTable")]
    public async Task<IActionResult> LoadTable([FromBody] DtParameters dtParameters)
    {
        /*var searchBy = dtParameters.Search?.Value;*/

        // if we have an empty search then just order the results by Id ascending
        var orderCriteria = "Id";
        var orderAscendingDirection = true;
        /*
        if (dtParameters.Order != null)
        {
            // in this example we just default sort on the 1st column
            orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
            orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
        }
        */
        var result = (await _service.GetAll()).AsQueryable();
        /*
        if (!string.IsNullOrEmpty(searchBy))
        {
            result = result.Where(r => r.Summary != null && r.Summary.ToUpper().Contains(searchBy.ToUpper()));
        }
        */
        //result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

        // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
        var filteredResultsCount = await result.CountAsync();
        var totalResultsCount = (await _service.GetAll()).Count();
        return Ok(result);
        /*
        return Ok(new
        {
            Draw = dtParameters.Draw,
            RecordsTotal = totalResultsCount,
            RecordsFiltered = filteredResultsCount,
            data = await result
                .Skip(dtParameters.Start)
                .Take(dtParameters.Length)
                .ToListAsync()
        });*/
    }
}
