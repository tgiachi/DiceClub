using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Entities.Cards;

namespace DiceClub.Database.Dto.Cards.Mappers;


[DtoMapper(typeof(CardLegalityEntity), typeof(CardLegalityDto))]
public class CardLegalityDtoMapper : AbstractDtoMapper<Guid, CardLegalityEntity, CardLegalityDto>
{
    public CardLegalityDtoMapper(IMapper mapper) : base(mapper)
    {
    }
}