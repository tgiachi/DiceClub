using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Cards;


namespace Mtg.Collection.Manager.Database.Entities
{

    [Table("color_card")]
    public class ColorCardEntity : BaseGuidEntity
    {
        public Guid CardId { get; set; }
        public virtual CardEntity Card { get; set; }

        public Guid ColorId { get; set; }
        public virtual ColorEntity Color { get; set; }
    }
}
