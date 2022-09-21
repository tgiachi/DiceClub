namespace DiceClub.Web.Data.Rest.Cards;

public class SearchAutoCompleteCard
{
    public int? MtgId { get; set; }
    public string Name { get; set; } = null!;

    public string SetCode { get; set; } = null!;
    public string? ImageUrl { get; set; }
}