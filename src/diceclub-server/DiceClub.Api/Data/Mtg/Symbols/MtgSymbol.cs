using System.Text.Json.Serialization;

namespace DiceClub.Api.Data.Mtg.Symbols;

public class MtgSymbol
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    
    [JsonPropertyName("svg_uri")]
    public string Image { get; set; }
    
    
    [JsonPropertyName("english")]
    public string Description { get; set; }
    
}