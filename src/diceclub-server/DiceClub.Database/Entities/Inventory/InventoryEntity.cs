using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Database.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace DiceClub.Database.Entities.Inventory
{

    [Table("inventories")]
    [Index(nameof(Sku), IsUnique = true)]
    public class InventoryEntity : BaseGuidEntity
    {

        [MaxLength(300)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public InventoryCategoryEntity  Category { get; set; }

        public Guid CategoryId { get; set; }

        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [MaxLength(300)]
        public string Sku { get; set; }

        public bool IsLocked { get; set; }

        public string? InventoryCode { get; set; }
    }
}
