using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards.Deck;

public class DeckMasterDto : AbstractGuidDtoEntity
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}