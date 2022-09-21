using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace DiceClub.Web.Controllers.WebSocket;

public class NotificationsHub : Hub, INotificationHandler<NotificationEvent>
{
    private readonly ILogger _logger;
    private readonly IHubContext<NotificationsHub> _hubContext;

    public NotificationsHub(ILogger<NotificationsHub> logger, IHubContext<NotificationsHub> hubContext)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public Task Handle(NotificationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending WebSocket notification type: {Type}", notification.Type);
        if (notification.IsBroadcast)
        {
            return _hubContext.Clients.All.SendAsync("notification", notification, cancellationToken);
        }

        return _hubContext.Clients.User(notification.UserId.ToString()).SendAsync("notification", cancellationToken);
    }
}