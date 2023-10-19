using MicroRabbitNet.Master.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbitNet.Master.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RabbitMqController : ControllerBase
{
    private readonly RabbitMqService _mqService;

    public RabbitMqController(RabbitMqService mqService)
    {
        _mqService = mqService;
    }

    [Route("[action]/{message}")]
    [HttpGet]
    public IActionResult SendMessage(string message)
    {
        _mqService.SendMessage(message);

        return Ok("Сообщение отправлено");
    }
}