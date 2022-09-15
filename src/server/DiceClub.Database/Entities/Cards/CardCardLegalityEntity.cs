using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Cards.Deck;

namespace DiceClub.Database.Entities.Cards;


[Table("card_card_legality")]
public class CardCardLegality : BaseGuidEntity
{
    public Guid CardId { get; set; }
    public virtual CardEntity Card { get; set; }

    public Guid CardLegalityId { get; set; }
    public virtual  CardLegalityEntity CardLegality { get; set; }
    
    public virtual CardLegalityTypeEntity CardLegalityType { get; set; }
    public Guid CardLegalityTypeId { get; set; }
    
}