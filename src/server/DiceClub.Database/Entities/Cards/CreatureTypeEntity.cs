using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Cards;


namespace Mtg.Collection.Manager.Database.Entities
{

    [Table("creatures_type")]
    public class CreatureTypeEntity : BaseGuidEntity
    {
        public string Name { get; set; }

       
        public virtual List<CardEntity> Cards { get; set; }
    }
}
