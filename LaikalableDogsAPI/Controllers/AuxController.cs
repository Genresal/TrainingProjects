using LaikableDogsAPI.Services.Interfaces;
using LaikalableDogsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaikalableDogsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuxController : ControllerBase
    {

        private readonly ILogger<AuxController> logger;
        private readonly IAuxService auxService;

        public AuxController(ILogger<AuxController> logger, IAuxService auxService)
        {
            this.logger = logger;
            this.auxService = auxService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDog()
        {
            await auxService.CreateDefaultData();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDog()
        {
            await auxService.DropCollections();

            return NoContent();
        }
    }
}