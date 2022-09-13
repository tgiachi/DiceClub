using MediatR;

namespace DiceClub.Api.Events.WebSocket
{
    public class DiagnosticMessageEvent : INotification
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public long? CurrentProgress { get; set; }
        public long? TotalProgress { get; set; }
    }

    public class DiagnosticMessageBuilder
    {
        private readonly DiagnosticMessageEvent _event;

        public static DiagnosticMessageBuilder Create()
        {
            return new DiagnosticMessageBuilder();
        }

        public DiagnosticMessageBuilder Message(string message)
        {
            _event.Message = message;
            return this;
        }

        public DiagnosticMessageBuilder Type(string type)
        {
            _event.Type = type;
            return this;
        }

        public DiagnosticMessageBuilder Progress(long current, long total)
        {
            _event.CurrentProgress = current;
            _event.TotalProgress = total;

            return this;
        }

        public DiagnosticMessageEvent Build()
        {
            return _event;
        }


    }
}
