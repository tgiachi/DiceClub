using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto
{
    public class InventoryDto : AbstractDtoEntity<Guid>
    {
        public string Name { get; set; }

        public string? Author { get; set; }
        public string Description { get; set; }

        public string SerialNumber { get; set; }

        public Guid? OwnerId { get; set; }
        
        public InventoryCategoryDto Category { get; set; }

        public string Image { get; set; }
    }
}
