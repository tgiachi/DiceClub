using Aurora.Api.Entities.Impl.Dto;
using Aurora.Api.Entities.Impl.Entities;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardStageDto : AbstractGuidDtoEntity
{
    public string ScryfallId { get; set; }
    public string CardName { get; set; }
    public string ImageUrl { get; set; }
    public Guid UserId { get; set; }
    public bool IsFoil { get; set; }
    public Guid LanguageId { get; set; }
    public bool IsAdded { get; set; }
    public int Quantity { get; set; }
}