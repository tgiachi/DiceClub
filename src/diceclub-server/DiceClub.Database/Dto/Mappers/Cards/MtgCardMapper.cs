using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Api.Data.Mtg;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;

[DtoMapper(typeof(MtgCardEntity), typeof(MtgCardDto))]
public class MtgCardMapper : AbstractDtoMapper<Guid, MtgCardEntity, MtgCardDto>
{
    public MtgCardMapper(IMapper mapper) : base(mapper)
    {
    }
}