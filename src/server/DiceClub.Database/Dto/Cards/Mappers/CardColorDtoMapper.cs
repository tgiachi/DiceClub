using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dto.Cards.Mappers
{

    [DtoMapper(typeof(ColorEntity), typeof(CardColorDto))]
    public class CardColorDtoMapper : AbstractDtoMapper<Guid, ColorEntity, CardColorDto>
    {
        public CardColorDtoMapper(IMapper mapper) : base(mapper)
        {

        }
    }
}
