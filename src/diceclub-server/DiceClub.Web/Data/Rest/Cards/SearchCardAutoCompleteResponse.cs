namespace DiceClub.Web.Data.Rest.Cards;

public class SearchCardAutoCompleteResponse
{
    public string SearchText { get; set; } = null!;

    public List<SearchAutoCompleteCard> AutoCompleteCards { get; set; } = null!;

}