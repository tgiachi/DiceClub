using Aurora.Api.Entities.Impl.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiceClub.Database.Entities.Inventory
{

    [Table("inventory_categories")]
    public class InventoryCategoryEntity : BaseGuidEntity
    {
        [MaxLength(300)]
        public string Name { get; set; }


        [MaxLength(400)]
        public string? ParserClassType { get; set; }
    }
}
