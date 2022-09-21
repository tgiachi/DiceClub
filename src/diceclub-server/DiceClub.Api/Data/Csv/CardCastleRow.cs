using CsvHelper.Configuration.Attributes;

namespace DiceClub.Api.Data;

public class CardCastleRow
{
    [Index(0)]
    public string CardName { get; set; }   
    
    [Index(1)]
    public string SetName { get; set; }
    
    [Index(2)]
    public string Condition { get; set; }
    
    [Index(3)]
    [BooleanTrueValues("true")]
    [BooleanFalseValues("false")]
    public bool IsFoil { get; set; }
    
    [Index(4)]
    public string Language { get; set; }
    
    [Index(5)]
    public long? MultiverseId { get; set; }
    
    [Index(6)]
    public string JsonId { get; set; }
    
    [Index(7)]
    public double? PriceUsd { get; set; }
}