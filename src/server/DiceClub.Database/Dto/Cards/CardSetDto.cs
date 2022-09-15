using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class CardSetDto : AbstractGuidDtoEntity
{
    public string SetCode { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}