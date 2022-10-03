using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards.Deck;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;

[DtoMapper(typeof(MtgCardSymbolEntity), typeof(MtgCardSymbolDto))]
public class MtgCardSymbolMapper : AbstractDtoMapper<Guid,MtgCardSymbolEntity, MtgCardSymbolDto>
{
    public MtgCardSymbolMapper(IMapper mapper) : base(mapper)
    {
    }
}