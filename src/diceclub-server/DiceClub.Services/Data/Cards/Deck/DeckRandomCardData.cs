using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Services.Data.Cards.Deck;

public class DeckRandomCardData
{
    public MtgCardTypeEntity Type { get; set; }
    public double Weight { get; set; }
    public int CardCount { get; set; }
}