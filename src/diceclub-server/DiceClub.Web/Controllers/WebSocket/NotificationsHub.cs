using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DiceClub.Web.Controllers.WebSocket;


[Authorize]
public class NotificationsHub : Hub, INotificationHandler<NotificationEvent>
{
    private readonly ILogger _logger;
    private readonly IHubContext<NotificationsHub> _hubContext;

    public NotificationsHub(ILogger<NotificationsHub> logger, IHubContext<NotificationsHub> hubContext)
    {
        _hubContext = hubContext;
        _logger = logger;
        
    }

    public override async Task OnConnectedAsync()
    {
        await _hubContext.Clients.All.SendAsync("notification",
            new NotificationEvent { Message = "test", Title = "test", Type = NotificationEventType.Information, });
    }

    public Task Handle(NotificationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending WebSocket notification type: {Type}", notification.Type);
        if (notification.IsBroadcast)
        {
            return _hubContext.Clients.All.SendAsync("notification", notification, cancellationToken);
        }

        return _hubContext.Clients.User(notification.UserId.ToString()).SendAsync("notification", notification, cancellationToken);
    }
}