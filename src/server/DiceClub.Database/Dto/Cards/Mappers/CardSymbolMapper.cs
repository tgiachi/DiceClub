using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Entities.Cards;

namespace DiceClub.Database.Dto.Cards.Mappers;


[DtoMapper(typeof(CardSymbolEntity), typeof(CardSymbolDto))]
public class CardSymbolMapper : AbstractDtoMapper<Guid, CardSymbolEntity, CardSymbolDto>
{
    public CardSymbolMapper(IMapper mapper) : base(mapper)
    {
        
    }
}