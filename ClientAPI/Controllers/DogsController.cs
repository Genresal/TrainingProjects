using System.Text.Json;
using ClientAPI.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace ClientAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogsController : ControllerBase
    {
        private readonly RestClient client;
        private readonly ILogger<DogsController> logger;

        public DogsController(ILogger<DogsController> logger)
        {
            logger = logger;
            client = new RestClient("https://localhost:7012/");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new RestRequest("api/dogs/all");
            var response = await client.ExecuteGetAsync(request);

            if (!response.IsSuccessful)
            {
                //Logic for handling unsuccessful response
            }
            var userDetails = JsonSerializer.Deserialize<IEnumerable<Dog>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            return Ok(userDetails);
        }
    }
}