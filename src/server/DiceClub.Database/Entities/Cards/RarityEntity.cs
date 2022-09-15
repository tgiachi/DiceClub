using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Cards
{

    [Table("rarity")]
    public class RarityEntity : BaseGuidEntity
    {
        public string Name { get; set; }
    }
}
