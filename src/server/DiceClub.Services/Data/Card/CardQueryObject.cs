namespace DiceClub.Services.Data.Card;

public class CardQueryObject
{
    public string? Description { get; set; }
    public string? Name { get; set; }
    public List<string>? Colors { get; set; }
    public List<string>? Rarity { get; set; }
    
    public List<string>? Types { get; set; }
}