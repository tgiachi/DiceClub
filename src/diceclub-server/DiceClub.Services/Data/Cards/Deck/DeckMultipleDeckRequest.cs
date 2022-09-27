namespace DiceClub.Services.Data.Cards.Deck;

public class DeckMultipleDeckRequest
{
    public int Count { get; set; }
    
    public int TotalCards { get; set; }
    
    public int SideBoardTotalCards { get; set; }
    public List<string> Colors { get; set; }
}