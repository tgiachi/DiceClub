using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TestAuthController : ControllerBase
    {


        [Route("test_auth")]
        [HttpGet]
        public async Task<IActionResult> TestAuth()
        {
            
            return Ok("");
        }
    }
}
