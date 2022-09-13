using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Entities.Cards;

namespace DiceClub.Database.Dto.Cards.Mappers;

[DtoMapper(typeof(CardSetEntity), typeof(CardSetDto))]
public class CardSetDtoMapper : AbstractDtoMapper<Guid, CardSetEntity ,CardSetDto>
{
    public CardSetDtoMapper(IMapper mapper) : base(mapper)
    {
    }
}