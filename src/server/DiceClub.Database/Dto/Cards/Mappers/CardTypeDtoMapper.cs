using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Entities.Cards;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dto.Cards.Mappers;

[DtoMapper(typeof(CardTypeEntity), typeof(CardTypeDto))]
public class CardTypeDtoMapper : AbstractDtoMapper<Guid, CardTypeEntity, CardTypeDto>
{
    public CardTypeDtoMapper(IMapper mapper) : base(mapper)
    {
    }
}