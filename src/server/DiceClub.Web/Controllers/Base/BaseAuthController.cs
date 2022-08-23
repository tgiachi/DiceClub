using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Base
{
    public class BaseAuthController : ControllerBase
    {

        protected Guid GetUserId()
        {
            var userId = User.Claims.FirstOrDefault(s => s.Type == "user_id");

            return Guid.Parse(userId.Value);
        }
    }
}
