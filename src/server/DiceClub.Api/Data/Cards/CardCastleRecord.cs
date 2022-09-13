using CsvHelper.Configuration.Attributes;

namespace DiceClub.Api.Data.Cards
{
    public class CardCastleRecord
    {
        [Name("Card Name")]
        public string Name { get; set; }

        [Name("Set Name")]
        public string SetName { get; set; }

        [Name("Condition")]
        public string Condition { get; set; }

        [Name("Foil")]
        public bool Foil { get; set; }

        [Name("Language")]
        public string Language { get; set; }

        [Name("Multiverse ID")]
        public long? MultiverseId { get; set; }

        [Name("JSON ID")]
        public string JsonId { get; set; }

        [Name("Price USD")]
        public decimal? Price { get; set; }
    }
}
