using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Movies.API.Controllers
{   
    [Route("api/[controller]")]
    [Controller]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
