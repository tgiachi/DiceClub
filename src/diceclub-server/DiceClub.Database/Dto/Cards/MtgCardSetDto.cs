using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardSetDto : AbstractGuidDtoEntity
{
    public string Code { get; set; }
    
    public string Description { get; set; }
    
    public string Image { get; set; }

    public int CardCount { get; set; }
}