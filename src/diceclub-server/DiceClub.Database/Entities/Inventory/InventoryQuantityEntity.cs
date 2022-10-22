using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Account;

namespace DiceClub.Database.Entities.Inventory
{

    [Table("inventory_quantities")]
    public class InventoryQuantityEntity : BaseGuidEntity
    {
        public Guid OwnerId { get; set; }

        public virtual DiceClubUser Owner { get; set; }

        public Guid InventoryId { get; set; }

        public virtual InventoryEntity Inventory { get; set; }

        public int Quantity { get; set; }
    }
}
