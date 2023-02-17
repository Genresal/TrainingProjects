using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _service;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(RecipeService service, ILogger<RecipeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<Recipe> Add([FromBody] Recipe viewModel)
        {
            var result = await _service.Add(viewModel);

            return result;
        }

        [HttpGet]
        public async Task<List<Recipe>> GetAll()
        {
            var result = await _service.GetAll();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<Recipe> Get(int id)
        {
            var result = await _service.Get(id);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }
    }
}