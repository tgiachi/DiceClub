using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Entities.Deck
{

    [Table("deck_details")]
    public class DeckDetailEntity : BaseGuidEntity
    {
        public Guid DeckMasterId { get; set; }
        public DeckMasterEntity DeckMaster { get; set; }
        public Guid CardId { get; set; }
        public virtual MtgCardEntity Card { get; set; }
        public long Quantity { get; set; }

        public DeckDetailCardType CardType { get; set; }
    }
}
