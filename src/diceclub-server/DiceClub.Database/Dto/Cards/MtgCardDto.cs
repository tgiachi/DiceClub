using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class MtgCardDto : AbstractGuidDtoEntity
{
    
    public string ScryfallId { get; set; }
    public int? MtgId { get; set; }
    public string Name { get; set; }
    public string ForeignNames { get; set; }
    public string PrintedName { get; set; }
    public string TypeLine { get; set; }
    public string Description { get; set; }
    public virtual MtgCardLanguageDto Language { get; set; }
    public string? ManaCost { get; set; }
    public decimal? Cmc { get; set; }
    public int? Power { get; set; }
    public int? Toughness { get; set; }
    public int? CollectorNumber { get; set; }
    public virtual MtgCardSetDto Set { get; set; }
    public virtual MtgCardRarityDto Rarity { get; set; }
    public string? LowResImageUrl { get; set; }
    public string? HighResImageUrl { get; set; }
    public virtual MtgCardTypeDto Type { get; set; }
        
    public int? CardMarketId { get; set; }
        
    public int Quantity { get; set; }

    public virtual List<MtgCardColorRelDto> Colors { get; set; }
    public virtual List<MtgCardLegalityRelDto> Legalities { get; set; }

    public Guid OwnerId { get; set; }
    
    public bool IsColorLess { get; set; }
    public bool IsMultiColor { get; set; }
        
    public decimal? Price { get; set; }
}