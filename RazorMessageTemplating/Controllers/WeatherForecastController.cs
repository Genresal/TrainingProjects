using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using RazorLight;

namespace RazorMessageTemplating.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IViewRender viewRender;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IViewRender viewRender)
        {
            _logger = logger;
            this.viewRender = viewRender;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        
        [HttpGet("GetFilledTemplate")]
        public async Task<IActionResult> GetFilledTemplate()
        {
            string template = "<div>Hi @Model.Name, the date and time is @System.DateTime.Now</div>";

            RazorLightEngine engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(Assembly.GetEntryAssembly())
                .Build();
            string result = await engine.CompileRenderStringAsync(
                "cacheKey",
                template,
                new
                {
                    Name = "SomeName"
                });

            return Ok(result);
        }

        [HttpGet("DefaultRazor")]
        public async Task<IActionResult> GetFilledTemplateWithDefaultRazor()
        {
            var bodyTxt = viewRender.RenderPartialViewToString("Index", "Hello!!!");
            return Ok(bodyTxt);
        }
    }
}