using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Account;

namespace DiceClub.Database.Entities.Inventory
{

    [Table("inventory")]
    public class Inventory : BaseGuidEntity
    {
        public string Name { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        public string SerialNumber { get; set; }

        public DiceClubUser? Owner { get; set; }

        public Guid CategoryId { get; set; }

        public InventoryCategory Category { get; set; }

        public string Image { get; set; }
    }
}
