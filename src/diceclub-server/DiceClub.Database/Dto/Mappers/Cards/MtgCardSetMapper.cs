using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;


[DtoMapper(typeof(MtgCardSetEntity), typeof(MtgCardSetDto))]
public class MtgCardSetMapper : AbstractDtoMapper<Guid, MtgCardSetEntity, MtgCardSetDto>
{
    public MtgCardSetMapper(IMapper mapper) : base(mapper)
    {
    }
}