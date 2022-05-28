using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Movies.API.Controllers
{   
    [Route("api/[controller]")]
    [Controller]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = from c in User.Claims select new { c.Type, c.Value };
            return Ok(result);
        }
    }
}
