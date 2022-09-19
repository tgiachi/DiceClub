namespace DiceClub.Web.Data.Rest;

public class SearchCardAutoCompleteResponse
{
    public string SearchText { get; set; } = null!;
    
    public List<SearchAutoCompleteCard> AutoCompleteCards { get; set; }

}