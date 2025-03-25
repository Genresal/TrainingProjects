using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Oauth2Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var user = User.Identity.Name;
            return Ok($"Authenticated as: {user}");
        }
    }
}
