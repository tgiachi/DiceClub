using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardColorRelDto : AbstractGuidDtoEntity
{
    public virtual MtgCardColorDto Color { get; set; }
}