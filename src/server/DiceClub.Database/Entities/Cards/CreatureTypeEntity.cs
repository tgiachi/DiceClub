using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards
{

    [Table("creatures_type")]
    public class CreatureTypeEntity : BaseGuidEntity
    {
        public string Name { get; set; }

       
        public virtual List<CardEntity> Cards { get; set; }
    }
}
