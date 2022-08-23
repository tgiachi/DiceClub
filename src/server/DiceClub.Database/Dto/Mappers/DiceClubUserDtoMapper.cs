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

    [DtoMapper(typeof(DiceClubUser), typeof(DiceClubUserDto))]
    public class DiceClubUserDtoMapper : AbstractDtoMapper<Guid, DiceClubUser, DiceClubUserDto>
    {
        public DiceClubUserDtoMapper(IMapper mapper) : base(mapper)
        {

        }
    }
}
