
using CsvHelper.Configuration.Attributes;

namespace DiceClub.Api.Data.Csv;

public class HelvaultProRow
{
    [Index(0)]
    public string CollectionNumber { get; set; }
    [Index(1)]
    public string EstimatedPrice { get; set; }
    [Index(2)]
    public string Extras { get; set; }
    [Index(3)]
    public string Language { get; set; }
    [Index(4)]
    public string Name { get; set; }
    [Index(5)]
    public string OracleId { get; set; }
    [Index(6)]
    public string Quantity { get; set; }
    [Index(7)]
    public string Rarity { get; set; }
    [Index(8)]
    public string ScryfallId { get; set; }
    [Index(9)]
    public string SetCode { get; set; }
    [Index(10)]
    public string SetName { get; set; }
    
}