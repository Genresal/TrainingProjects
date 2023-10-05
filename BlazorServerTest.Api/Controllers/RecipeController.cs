using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories;
using BlazorServerTestApi.VIewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerTestApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("[controller]")]
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

            var result = await _repository.AddAsync(viewModel.Adapt<Recipe>());

            return result.Adapt<RecipeViewModel>();
        }

        [HttpGet]
        public async Task<List<RecipeViewModel>> GetAll()
        {
            var result = new List<RecipeViewModel>();

            return result.Adapt<List<RecipeViewModel>>();
        }

        [HttpGet("{id}")]
        public async Task<RecipeViewModel> Get(int id)
        {
            var result = await _repository.FirstOrDefaultAsync<Recipe>(x => x.Id == id, null, true, CancellationToken.None);

            return result.Adapt<RecipeViewModel>();
        }
    }
}