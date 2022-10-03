using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;

[DtoMapper(typeof(MtgCardColorRelEntity), typeof(MtgCardColorRelDto))]
public class MtgCardColorRelMapper : AbstractDtoMapper<Guid, MtgCardColorRelEntity, MtgCardColorRelDto>
{
    public MtgCardColorRelMapper(IMapper mapper) : base(mapper)
    {
    }
}