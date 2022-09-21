using DiceClub.Api.Data.Rest;

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
        public Task<RestResultObject<string>> GeneratePassword([FromQuery] string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var generatedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt) + "#" + salt;
            return Task.FromResult(RestResultObjectBuilder<string>.Create().Data(generatedPassword).Build());
        }
    }
}