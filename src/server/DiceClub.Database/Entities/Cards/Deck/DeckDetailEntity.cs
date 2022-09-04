using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards.Deck
{

    [Table("deck_details")]
    public class DeckDetailEntity : BaseGuidEntity
    {
        public Guid DeckMasterId { get; set; }
        public DeckMasterEntity DeckMaster { get; set; }
        public Guid CardId { get; set; }
        public virtual CardEntity Card { get; set; }
        public long Quantity { get; set; }
    }
}
