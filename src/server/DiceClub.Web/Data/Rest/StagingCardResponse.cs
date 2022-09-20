namespace DiceClub.Web.Data.Rest;

public class StagingCardResponse
{
    public Guid Id { get; set; }
    public int? MtgId { get; set; }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    
    public string SetCode { get; set; }
    
}