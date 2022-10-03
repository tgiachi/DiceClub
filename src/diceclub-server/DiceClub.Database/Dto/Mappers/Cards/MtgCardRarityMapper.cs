using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;

[DtoMapper(typeof(MtgCardRarityEntity), typeof(MtgCardRarityDto))]
public class MtgCardRarityMapper : AbstractDtoMapper<Guid, MtgCardRarityEntity, MtgCardRarityDto>
{
    public MtgCardRarityMapper(IMapper mapper) : base(mapper)
    {
    }
}