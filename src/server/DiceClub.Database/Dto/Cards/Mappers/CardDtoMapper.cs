using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Entities.Cards;

namespace DiceClub.Database.Dto.Cards.Mappers;

[DtoMapper(typeof(CardEntity), typeof(CardDto))]
public class CardDtoMapper : AbstractDtoMapper<Guid, CardEntity, CardDto>
{
    public CardDtoMapper(IMapper mapper) : base(mapper)
    {
    }
}