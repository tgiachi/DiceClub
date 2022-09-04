using System.ComponentModel.DataAnnotations.Schema;
using Aurora.Api.Entities.Impl.Entities;


namespace Mtg.Collection.Manager.Database.Entities
{
    [Table("colors")]
    public class ColorEntity : BaseGuidEntity
    {
        public string Name { get; set; }

        public virtual List<ColorCardEntity> ColorCards { get; set; }
    }
}
