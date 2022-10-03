using System.Text.Json.Serialization;

namespace DiceClub.Api.Data.Mtg.Symbols;

public class MtgResultData<TData> 
{
    
    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
    [JsonPropertyName("data")]
    public List<TData> Data { get; set; }
}