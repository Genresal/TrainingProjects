using LaikableDogsAPI.Models.Requests;
using LaikableDogsAPI.Services.Interfaces;
using LaikalableDogsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaikalableDogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogsController : ControllerBase
    {

        private readonly ILogger<DogsController> logger;
        private readonly IDogService dogService;

        public DogsController(ILogger<DogsController> logger, IDogService dogService)
        {
            this.logger = logger;
            this.dogService = dogService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await dogService.GetAllDogs());
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetSortedDogs([FromQuery]SortingRequest request)
        {
            return Ok(await dogService.GetSortedDogs(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDogById(Guid id)
        {
            return Ok(await dogService.GetDogById(id));
        }
        
        [HttpGet("friends/{name}")]
        public async Task<IActionResult> GetDogFriends(string name)
        {
            return Ok(await dogService.GetDogFriends(name));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateDog(Dog request)
        {
            await dogService.CreateDog(request);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDog(Guid id, Dog request)
        {
            await dogService.UpdateDog(id, request);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDog(Guid id)
        {
            await dogService.DeleteDog(id);

            return NoContent();
        }
    }
}