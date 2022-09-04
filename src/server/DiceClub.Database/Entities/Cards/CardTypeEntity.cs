using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Cards;


namespace Mtg.Collection.Manager.Database.Entities
{
    [Table("card_types")]
    public class CardTypeEntity : BaseGuidEntity
    {
        public string CardType { get; set; }

        public virtual List<CardEntity> Cards { get; set; }
    }
}
