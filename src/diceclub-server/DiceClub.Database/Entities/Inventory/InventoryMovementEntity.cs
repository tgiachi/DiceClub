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

    [Table("inventory_movements")]
    public class InventoryMovementEntity : BaseGuidEntity
    {
        public Guid InventoryId { get; set; }

        public InventoryEntity Inventory { get; set; }

        public InventoryMovementStatusType Status { get; set; }

        public Guid SenderId { get; set; }

        public DiceClubUser Sender { get; set; }

        public Guid ReceiverId { get; set; }

        public DiceClubUser Receiver { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
