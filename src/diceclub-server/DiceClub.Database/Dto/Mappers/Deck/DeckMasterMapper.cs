using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dto.Cards.Deck;
using DiceClub.Database.Entities.Deck;

namespace DiceClub.Database.Dto.Mappers.Deck;


[DtoMapper(typeof(DeckMasterEntity), typeof(DeckMasterDto))]
public class DeckMasterMapper : AbstractDtoMapper<Guid, DeckMasterEntity, DeckMasterDto>
{
    public DeckMasterMapper(IMapper mapper) : base(mapper)
    {
        
    }
}