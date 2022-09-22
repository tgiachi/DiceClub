using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;


[DtoMapper(typeof(MtgCardLegalityRelEntity), typeof(MtgCardLegalityRelDto))]
public class MtgCardLegalityRelMapper : AbstractDtoMapper<Guid, MtgCardLegalityRelEntity, MtgCardLegalityRelDto>
{
    public MtgCardLegalityRelMapper(IMapper mapper) : base(mapper)
    {
    }
}