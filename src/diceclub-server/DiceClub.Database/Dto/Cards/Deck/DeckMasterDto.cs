using Aurora.Api.Entities.Impl.Dto;
using DiceClub.Database.Entities.Deck;

namespace DiceClub.Database.Dto.Cards.Deck;

public class DeckMasterDto : AbstractGuidDtoEntity
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public string ColorIdentity { get; set; }
        
    public DeckFormat Format { get; set; }
    
    public int CardCount { get; set; }
}