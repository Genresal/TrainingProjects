using CloudinaryDotNet.Actions;
using CloudinaryWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudinaryWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CloudinaryController : Controller
{
    private readonly ICloudinaryService _cloudinaryService;

    public CloudinaryController(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var imageUrl = await _cloudinaryService.UploadImageAsync(file);

        if (imageUrl is null)
            return BadRequest();

        return Ok(new { imageUrl });
    }

    [HttpGet("{publicId}")]
    public async Task<IActionResult> GetImageInfo([FromRoute] string publicId)
    {
        try
        {
            var imageInfo = await _cloudinaryService.GetImageInfoAsync(publicId);
            return Ok(imageInfo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetImages()
    {
        return Ok(await _cloudinaryService.GetImagesAsync());
    }

    [HttpDelete("{publicId}")]
    public async Task<IActionResult> Delete([FromRoute] string publicId)
    {
        try
        {
            publicId = publicId.Replace("%2F", "/");

            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinaryService.DeleteAsync(deleteParams);

            if (result.Result == "ok")
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error.Message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("images/{publicId}")]
    public IActionResult GetResizedImage(string publicId, [FromQuery] int width, [FromQuery] int height)
    {
        var imageUrl = _cloudinaryService.Resize(publicId, width, height);

        return Ok(imageUrl);
    }

    [HttpGet("url")]
    public IActionResult GetTemporaryUrl()
    {
        var imageUrl = _cloudinaryService.GetTemporaryUrl();

        return Ok(imageUrl);
    }

    [HttpGet("urlPup")]
    public IActionResult GetTemporaryPubUrl()
    {
        var imageUrl = _cloudinaryService.GetTemporaryUrlForPublic();

        return Ok(imageUrl);
    }
}
