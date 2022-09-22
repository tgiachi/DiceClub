using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;

[DtoMapper(typeof(MtgCardLanguageEntity), typeof(MtgCardLanguageDto))]
public class MtgCardLanguageMapper : AbstractDtoMapper<Guid, MtgCardLanguageEntity, MtgCardLanguageDto>
{
    public MtgCardLanguageMapper(IMapper mapper) : base(mapper)
    {
    }
}