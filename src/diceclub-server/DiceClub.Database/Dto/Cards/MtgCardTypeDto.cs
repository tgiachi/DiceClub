using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardTypeDto : AbstractGuidDtoEntity
{
    public string Name { get; set; }
}