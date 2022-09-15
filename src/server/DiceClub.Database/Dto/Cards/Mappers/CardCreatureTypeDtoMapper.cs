using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Entities.Cards;

namespace DiceClub.Database.Dto.Cards.Mappers;


[DtoMapper(typeof(CreatureTypeEntity), typeof(CreatureTypeDto))]
public class CardCreatureTypeDtoMapper : AbstractDtoMapper<Guid, CreatureTypeEntity, CreatureTypeDto>
{
    public CardCreatureTypeDtoMapper(IMapper mapper) : base(mapper)
    {
    }
}