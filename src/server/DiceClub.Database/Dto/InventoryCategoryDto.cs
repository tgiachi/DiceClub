using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto
{
    public class InventoryCategoryDto : AbstractDtoEntity<Guid>
    {
        public string Name { get; set; }

        public string? ParserType { get; set; }
    }
}
