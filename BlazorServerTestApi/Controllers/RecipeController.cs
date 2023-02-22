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
        public async Task<RecipeViewModel> Add([FromBody] ChangeRecipeViewModel viewModel)
        {

            var result = await _repository.Add(viewModel.Adapt<Recipe>(), viewModel.CategoryIds);

            return result.Adapt<RecipeViewModel>();
        }

        [HttpGet]
        public async Task<List<RecipeViewModel>> GetAll()
        {
            var result = await _repository.GetAll();

            return result.Adapt<List<RecipeViewModel>>();
        }

        [HttpGet("{id}")]
        public async Task<RecipeViewModel> Get(int id)
        {
            var result = await _repository.Get(id);

            return result.Adapt<RecipeViewModel>();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}