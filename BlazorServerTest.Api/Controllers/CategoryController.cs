using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Data.Repositories;
using InMemoryCachingLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _repository;
        private readonly ICacheService _cacheService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CategoryRepository repository, ICacheService cacheService, ILogger<CategoryController> logger)
        {
            _repository = repository;
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<Category> Add([FromBody] Category viewModel)
        {
            var result = await _repository.Add(viewModel);

            return result;
        }

        [HttpGet("cache")]
        public async Task<List<Category>> GetAllWithCache()
        {
            var key = nameof(Category);

            var result = await _cacheService.GetOrCreateAsync(key, _repository.GetAll);

            return result;
        }

        [HttpGet]
        public async Task<List<Category>> GetAll()
        {
            var result = await _repository.GetAll();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
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