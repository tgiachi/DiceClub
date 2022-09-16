using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class CardSymbolDto : AbstractGuidDtoEntity
{
    public string Symbol { get; set; }
    
    public string Image { get; set; }
}