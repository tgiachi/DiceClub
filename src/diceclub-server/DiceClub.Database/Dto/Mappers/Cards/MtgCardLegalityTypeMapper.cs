using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Database.Dto.Mappers.Cards;


[DtoMapper(typeof(MtgCardLegalityTypeEntity), typeof(MtgCardLegalityTypeDto))]
public class MtgCardLegalityTypeMapper : AbstractDtoMapper<Guid, MtgCardLegalityTypeEntity, MtgCardLegalityTypeDto>
{
    public MtgCardLegalityTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}