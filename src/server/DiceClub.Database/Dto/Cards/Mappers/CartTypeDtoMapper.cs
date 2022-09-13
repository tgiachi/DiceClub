using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dto.Cards.Mappers;

[DtoMapper(typeof(CardTypeEntity), typeof(CardTypeDto))]
public class CartTypeDtoMapper : AbstractDtoMapper<Guid, CardTypeEntity, CardTypeDto>
{
    public CartTypeDtoMapper(IMapper mapper) : base(mapper)
    {
    }
}