using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards.Deck;
using DiceClub.Database.Entities.Deck;

namespace DiceClub.Database.Dto.Mappers.Deck;

[DtoMapper(typeof(DeckDetailEntity), typeof(DeckDetailDto))]
public class DeckDetailMapper : AbstractDtoMapper<Guid, DeckDetailEntity, DeckDetailDto>
{
    public DeckDetailMapper(IMapper mapper) : base(mapper)
    {
        
    }
}