using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards.Deck
{

    [Table("deck_master")]
    public class DeckMasterEntity : BaseGuidEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual List<DeckDetailEntity> Details { get; set; }
    }
}
