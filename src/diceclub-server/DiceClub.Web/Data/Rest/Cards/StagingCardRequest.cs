namespace DiceClub.Web.Data.Rest.Cards;

public class StagingCardRequest
{
    public int MtgId { get; set; }

    public string Language { get; set; } = null!;

    public bool IsFoil { get; set; }
}