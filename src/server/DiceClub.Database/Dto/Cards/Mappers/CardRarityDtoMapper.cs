using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dto.Cards.Mappers;

[DtoMapper(typeof(RarityEntity), typeof(CardRarityDto))]
public class CardRarityDtoMapper : AbstractDtoMapper<Guid, RarityEntity, CardRarityDto>
{
    public CardRarityDtoMapper(IMapper mapper) : base(mapper)
    {
    }
}