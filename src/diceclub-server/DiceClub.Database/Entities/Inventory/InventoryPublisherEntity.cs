using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Entities.Inventory
{
    [Table("inventory_publishers")]
    public class InventoryPublisherEntity : BaseGuidEntity
    {
        public string Name { get; set; }
    }
}
