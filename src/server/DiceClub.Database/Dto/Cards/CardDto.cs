using Aurora.Api.Entities.Impl.Dto;

namespace DiceClub.Database.Dto.Cards;

public class CardDto : AbstractGuidDtoEntity
{
    public string CardName { get; set; }

    public string ManaCost { get; set; }

    public int TotalManaCosts { get; set; }

    public int? MtgId { get; set; }

    public decimal? Price { get; set; }

    public int Quantity { get; set; }

    public string ImageUrl { get; set; }

    public virtual List<CardColorDto> ColorCards { get; set; }
    public Guid CardTypeId { get; set; }
    public virtual CardTypeDto CardType { get; set; }

    public Guid RarityId { get; set; }

    public virtual CardRarityDto Rarity { get; set; }

    public Guid? CreatureTypeId { get; set; }
    public virtual CardTypeDto CreatureType { get; set; }
        
    public Guid CardSetId { get; set; }
        
    public virtual CardSetDto CardSet { get; set; }

    public Guid UserId { get; set; }

    public DiceClubUserDto User { get; set; }
    
    public string Description { get; set; }
    
}