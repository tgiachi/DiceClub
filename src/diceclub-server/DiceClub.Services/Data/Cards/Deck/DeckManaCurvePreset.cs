using DiceClub.Web.Data.Rest.Cards.Deck;

namespace DiceClub.Services.Data.Cards.Deck;

public class DeckManaCurvePreset
{
    public string Name { get; set; }
    
    public List<DeckManaCurve> ManaCurve { get; set; }
}