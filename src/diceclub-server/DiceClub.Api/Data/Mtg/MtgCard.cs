namespace DiceClub.Api.Data.Mtg;

public class MtgCard 
{
    public string Artist { get; set; }


    public string Border { get; set; }


    public float? Cmc { get; set; }


    public string[] ColorIdentity { get; set; }


    public string[] Colors { get; set; }


    public string Flavor { get; set; }


    public List<MtgForeignName> ForeignNames { get; set; }


    public string Hand { get; set; }


    public string Id { get; set; }


    public Uri ImageUrl { get; set; }


    public bool IsColorless => (Colors?.Length ?? 0) < 1;


    public bool IsMultiColor => Colors?.Length > 1;


    public string Layout { get; set; }


    public List<MtgLegality> Legalities { get; set; }


    public string Life { get; set; }


    public string Loyalty { get; set; }


    public string ManaCost { get; set; }


    public string MultiverseId { get; set; }


    public string Name { get; set; }


    public string[] Names { get; set; }


    public string Number { get; set; }


    public string OriginalText { get; set; }


    public string OriginalType { get; set; }


    public string Power { get; set; }


    public string[] Printings { get; set; }


    public string Rarity { get; set; }


    public string ReleaseDate { get; set; }


    public bool? Reserved { get; set; }


    public List<MtgRuling> Rulings { get; set; }


    public string Set { get; set; }


    public string SetName { get; set; }


    public string Source { get; set; }


    public bool? Starter { get; set; }


    public string[] SubTypes { get; set; }


    public string[] SuperTypes { get; set; }


    public string Text { get; set; }


    public bool? Timeshifted { get; set; }


    public string Toughness { get; set; }


    public string Type { get; set; }


    public string[] Types { get; set; }


    public string[] Variations { get; set; }


    public string Watermark { get; set; }
}