using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Account;
using DiceClub.Database.Entities.Account;

namespace DiceClub.Database.Dto.Mappers.Account;

[DtoMapper(typeof(DiceClubUser), typeof(DiceClubUserDto))]
public class DiceClubUserMapper : AbstractDtoMapper<Guid, DiceClubUser, DiceClubUserDto>
{
    public DiceClubUserMapper(IMapper mapper) : base(mapper)
    {
    }
}