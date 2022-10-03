using Aurora.Api.Entities.Impl.Dto;
using DiceClub.Database.Entities.Deck;

namespace DiceClub.Database.Dto.Cards.Deck;

public class DeckDetailDto : AbstractGuidDtoEntity
{
    public Guid DeckMasterId { get; set; }
    public Guid CardId { get; set; }
    public virtual MtgCardDto Card { get; set; }
    public long Quantity { get; set; }
    public DeckDetailCardType CardType { get; set; }
}