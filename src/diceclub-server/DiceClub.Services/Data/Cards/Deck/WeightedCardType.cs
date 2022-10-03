using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Services.Data.Cards.Deck;

public class WeightedCardType : IWeighted
{
    public MtgCardTypeEntity CardType { get; set; }
    public int Weight { get; set; }
}