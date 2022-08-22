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
    public class InventoryMovement : BaseGuidEntity
    {
        public Guid InventoryId { get; set; }

        public virtual Inventory Inventory { get; set; }

        public Guid ReceiverId { get; set; }

        public DiceClubUser Receiver { get; set; }

        public DateTime ExpireDateTime { get; set; }

        public InventoryMovementType MovementType { get; set; }

        public Guid ApproverId { get; set; }

        public DiceClubUser Approver { get; set; }

        public bool IsApproved { get; set; }

    }

    public enum InventoryMovementType
    {
        NotAvailable,
        Borrowed,
        Available
    }
}
