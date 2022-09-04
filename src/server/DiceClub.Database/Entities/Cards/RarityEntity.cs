using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;


namespace Mtg.Collection.Manager.Database.Entities
{

    [Table("rarity")]
    public class RarityEntity : BaseGuidEntity
    {
        public string Name { get; set; }
    }
}
