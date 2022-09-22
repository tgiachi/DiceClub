using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardLegalityRelDto : AbstractGuidDtoEntity
{
    public virtual  MtgCardLegalityDto CardLegality { get; set; }
    public virtual MtgCardLegalityTypeDto CardLegalityType { get; set; }
 
}