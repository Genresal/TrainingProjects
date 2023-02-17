using BlazorServerTest.Core.Data.Entities;
using BlazorServerTest.Core.Services;
using InMemoryCachingLibrary;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;
        private readonly ICacheService _cacheService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(CategoryService service, ICacheService cacheService, ILogger<CategoryController> logger)
        {
            _service = service;
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<Category> Add([FromBody] Category viewModel)
        {
            var result = await _service.Add(viewModel);

            return result;
        }

        [HttpGet("cache")]
        public async Task<List<Category>> GetAllWithCache()
        {
            var key = nameof(Category);

            var result = await _cacheService.GetOrCreateAsync(key, _service.GetAll);

            return result;
        }

        [HttpGet]
        public async Task<List<Category>> GetAll()
        {
            var result = await _service.GetAll();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
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