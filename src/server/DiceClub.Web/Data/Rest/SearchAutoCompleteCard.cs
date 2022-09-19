namespace DiceClub.Web.Data.Rest;

public class SearchAutoCompleteCard 
{
    public int? MtgId { get; set; }
    public string Name { get; set; }
    
    public string SetCode { get; set; }
    public string? ImageUrl { get; set; }
}