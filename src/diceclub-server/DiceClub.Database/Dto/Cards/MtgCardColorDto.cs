using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardColorDto : AbstractGuidDtoEntity
{
  
    public string Name { get; set; }

    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
}