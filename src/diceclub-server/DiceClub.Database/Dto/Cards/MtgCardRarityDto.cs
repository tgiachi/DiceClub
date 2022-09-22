using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardRarityDto : AbstractGuidDtoEntity
{
    public string Name { get; set; }
    
    public string? Image { get; set; }
}