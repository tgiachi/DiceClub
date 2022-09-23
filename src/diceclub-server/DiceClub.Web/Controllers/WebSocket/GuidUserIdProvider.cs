using Microsoft.AspNetCore.SignalR;

namespace DiceClub.Web.Controllers.WebSocket;

public class GuidUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst("user_id")?.Value;
    }
}