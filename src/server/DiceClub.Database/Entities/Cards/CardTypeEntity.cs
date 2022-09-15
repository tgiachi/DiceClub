using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards
{
    [Table("card_types")]
    public class CardTypeEntity : BaseGuidEntity
    {
        public string CardType { get; set; }

        public virtual List<CardEntity> Cards { get; set; }
    }
}
