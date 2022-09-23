using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;

[DtoMapper(typeof(MtgCardStageEntity), typeof(MtgCardStageDto))]
public class MtgCardStageMapper : AbstractDtoMapper<Guid, MtgCardStageEntity, MtgCardStageDto>
{
    public MtgCardStageMapper(IMapper mapper) : base(mapper)
    {
    }
}