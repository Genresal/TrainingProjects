using Microsoft.AspNetCore.Mvc;
using RabbitMQProducer.RabbitMQ;

namespace RabbitMQProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController : ControllerBase
    {
        private readonly IRabbitMqService _mqService;

        public RabbitMqController(IRabbitMqService mqService)
        {
            _mqService = mqService;
        }

        [Route("[action]/{message}")]
        [HttpGet]
        public IActionResult SendMessage(string message)
        {
            _mqService.SendMessage(message);

            return Ok("Message sends");
        }
    }
}