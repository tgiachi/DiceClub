using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Utils
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class UtilsController : ControllerBase
    {


        [HttpGet]
        [Route("generate_password")]
        public async Task<object> GeneratePassword([FromQuery] string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var generatedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt) + "#" + salt;
            return new
            {
                Password = generatedPassword
            };
        }
    }
}
