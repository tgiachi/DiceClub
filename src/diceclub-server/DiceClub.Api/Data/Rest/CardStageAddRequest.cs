namespace DiceClub.Api.Data.Rest;

public class CardStageAddRequest
{
    public string CardName { get; set; }
    public string SetCode { get; set; }
    public Guid LanguageId { get; set; }
    public int Quantity { get; set; } = 1;
    public bool IsFoil { get; set; } = false;
}