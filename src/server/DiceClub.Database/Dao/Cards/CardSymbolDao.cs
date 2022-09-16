using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards;

[DataAccess]
public class CardSymbolDao : AbstractDataAccess<Guid, CardSymbolEntity, DiceClubDbContext>
{
    public CardSymbolDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CardSymbolEntity> logger) : base(
        dbContext, logger)
    {
    }

    public async Task<CardSymbolEntity> CreateIfNotExists(string symbol, string image)
    {
        var cardSymbol = await QueryAsSingle(entities => entities.Where(s => s.Symbol == symbol));
        if (cardSymbol == null)
        {
            cardSymbol = new CardSymbolEntity
            {
                Symbol = symbol,
                Image = image
            };

            return await Insert(cardSymbol);
        }

        return cardSymbol;
    }
}