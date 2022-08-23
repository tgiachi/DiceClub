using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto
{
    public class DiceClubGroupDto : AbstractDtoEntity<Guid>
    {
        public string GroupName { get; set; }

        public bool IsAdmin { get; set; }
    }
}
