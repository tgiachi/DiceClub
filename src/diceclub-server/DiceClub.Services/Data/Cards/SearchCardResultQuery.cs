using DiceClub.Database.Entities.MtgCards;

namespace DiceClub.Services.Data.Cards;

public class SearchCardResultQuery
{
    public List<MtgCardEntity> Cards { get; set; }

    public long TotalCount { get; set; }
}