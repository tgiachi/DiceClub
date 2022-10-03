using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;

[DtoMapper(typeof(MtgCardColorEntity), typeof(MtgCardColorDto))]
public class MtgCardColorMapper : AbstractDtoMapper<Guid, MtgCardColorEntity, MtgCardColorDto>
{
    public MtgCardColorMapper(IMapper mapper) : base(mapper)
    {
    }
}