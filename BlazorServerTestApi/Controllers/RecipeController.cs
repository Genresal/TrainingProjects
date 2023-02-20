using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories;
using BlazorServerTestApi.VIewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeRepository _repository;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(RecipeRepository repository, ILogger<RecipeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<Recipe> Add([FromBody] ChangeRecipeViewModel viewModel)
        {

            var result = await _repository.Add(viewModel.Adapt<Recipe>(), viewModel.CategoryIds);

            return result;
        }

        [HttpGet]
        public async Task<List<Recipe>> GetAll()
        {
            var result = await _repository.GetAll();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<Recipe> Get(int id)
        {
            var result = await _repository.Get(id);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}