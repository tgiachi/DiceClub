using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Inventory
{

    [Table("inventory_category")]
    public class InventoryCategory : BaseGuidEntity
    {
        public string Name { get; set; }

        public virtual List<Inventory> Inventories { get; set; }
    }
}
