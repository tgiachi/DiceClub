using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Entities.Cards;

namespace DiceClub.Database.Dto.Cards.Mappers;


[DtoMapper(typeof(CardLegalityTypeEntity), typeof(CardLegalityTypeDto))]
public class CardLegalityTypeMapper : AbstractDtoMapper<Guid, CardLegalityTypeEntity, CardLegalityTypeDto>
{
    public CardLegalityTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}