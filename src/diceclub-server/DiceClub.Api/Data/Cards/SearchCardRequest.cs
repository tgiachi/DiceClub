using System.Runtime.Serialization;

namespace DiceClub.Api.Data.Cards;

public class SearchCardRequest
{
    public string? Description { get; set; }
    public List<string>? Colors { get; set; }
    
    public SearchCardRequestOrderBy? OrderBy { get; set; }
}

public enum SearchCardRequestOrderBy
{
    Set,
    Price,
    Name,
    Rarity,
    CardType,
    CreatedDate,
    Quantity
}