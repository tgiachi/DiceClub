using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards.Deck;

public class MtgCardSymbolDto : AbstractGuidDtoEntity
{  
    public string Symbol { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    
}