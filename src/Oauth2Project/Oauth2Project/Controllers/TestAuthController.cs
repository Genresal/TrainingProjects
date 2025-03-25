using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Oauth2Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestAuthController : ControllerBase
    {
        // This endpoint requires authentication
        [HttpGet("secure")]
        [Authorize]
        [RequiredScope("access_as_user")]
        public IActionResult GetSecureData()
        {
            return Ok(new
            {
                Message = "This is secure data!",
                TimeStamp = DateTime.UtcNow
            });
        }

        // This endpoint returns the authenticated user's claims
        [HttpGet("claims")]
        [Authorize]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
                Claims = claims
            });
        }

        // This endpoint is publicly accessible
        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult GetPublicData()
        {
            return Ok(new
            {
                Message = "This is public data that doesn't require authentication",
                TimeStamp = DateTime.UtcNow
            });
        }
    }
}
