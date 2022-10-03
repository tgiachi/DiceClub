using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardLanguageDto : AbstractGuidDtoEntity
{
    public string Name { get; set; }

    public string Code { get; set; }

}