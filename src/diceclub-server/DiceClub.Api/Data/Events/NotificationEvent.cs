using MediatR;

public class NotificationEvent : INotification
{
    public bool IsBroadcast { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }

    public NotificationEventType Type { get; set; }
    public int CurrentProgress { get; set; }
    public int MaxProgress { get; set; }
    public Guid UserId { get; set; }
}

public enum NotificationEventType
{
    Information,
    Warning,
    Error
}

public class NotificationEventBuilder
{
    private readonly NotificationEvent _event = new();

    public static NotificationEventBuilder Create()
    {
        return new NotificationEventBuilder();
    }

    public NotificationEvent Build()
    {
        return _event;
    }

    public NotificationEventBuilder Type(NotificationEventType type)
    {
        _event.Type = type;
        return this;
    }

    public NotificationEventBuilder Title(string title)
    {
        _event.Title = title;
        return this;
    }

    public NotificationEventBuilder Broadcast()
    {
        _event.IsBroadcast = true;
        return this;
    }

    public NotificationEventBuilder Progress(int current, int max)
    {
        _event.CurrentProgress = current;
        _event.MaxProgress = max;
        return this;
    }

    public NotificationEventBuilder Message(string message)
    {
        _event.Message = message;
        return this;
    }

    public NotificationEventBuilder ToUser(Guid userId)
    {
        _event.UserId = userId;
        return this;
    }
}