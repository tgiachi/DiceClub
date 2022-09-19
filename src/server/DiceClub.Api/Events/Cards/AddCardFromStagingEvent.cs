using MediatR;

namespace DiceClub.Api.Events.Cards;

public class AddCardFromStagingEvent : INotification
{
    public Guid StagingId { get; set; }
    
    public string Language { get; set; }
}