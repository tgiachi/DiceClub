using DiceClub.Database.Dao.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DiceClub.Web.Controllers.WebSocket;


[Authorize]
public class NotificationsHub : Hub, INotificationHandler<NotificationEvent>
{
    private readonly ILogger _logger;
    private readonly IHubContext<NotificationsHub> _hubContext;
    private DiceClubUserDao _diceClubUserDao;

    public NotificationsHub(ILogger<NotificationsHub> logger, IHubContext<NotificationsHub> hubContext, DiceClubUserDao diceClubUserDao)
    {
        _hubContext = hubContext;
        _diceClubUserDao = diceClubUserDao;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User.Claims.ToList().FirstOrDefault(s => s.Type == "user_id").Value;
        _logger.LogInformation("Connected userId: {Id}", userId);
        var user = await _diceClubUserDao.FindById(new Guid(userId));
        await Clients.Caller.SendAsync("notification",
            new NotificationEvent { Message = $"Ciao <strong>{user.NickName}</strong>", Title = "Welcome", Type = NotificationEventType.Information, });
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