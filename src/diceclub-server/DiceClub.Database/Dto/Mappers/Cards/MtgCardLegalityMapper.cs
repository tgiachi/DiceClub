using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;

[DtoMapper(typeof(MtgCardLegalityEntity), typeof(MtgCardLegalityDto))]
public class MtgCardLegalityMapper : AbstractDtoMapper<Guid, MtgCardLegalityEntity, MtgCardLegalityDto>
{
    public MtgCardLegalityMapper(IMapper mapper) : base(mapper)
    {
    }
}