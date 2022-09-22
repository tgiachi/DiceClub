using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;


[DtoMapper(typeof(MtgCardTypeEntity), typeof(MtgCardTypeDto))]
public class MtgCardTypeMapper : AbstractDtoMapper<Guid, MtgCardTypeEntity, MtgCardTypeDto>
{
    public MtgCardTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}