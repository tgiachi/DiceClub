using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Entities.Account;

namespace DiceClub.Database.Dto.Mappers
{

    [DtoMapper(typeof(DiceClubGroup), typeof(DiceClubGroupDto))]
    public class DiceClubGroupDtoMapper : AbstractDtoMapper<Guid, DiceClubGroup, DiceClubGroupDto>
    {
        public DiceClubGroupDtoMapper(IMapper mapper) : base(mapper)
        {

        }
    }
}
