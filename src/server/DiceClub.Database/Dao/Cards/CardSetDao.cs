using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Account;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards;


[DataAccess]
public class CardSetDao : AbstractDataAccess<Guid, CardSetEntity, DiceClubDbContext>
{
    public CardSetDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CardSetEntity> logger) : base(dbContext, logger)
    {
        
    }

    public async Task<CardSetEntity> CreateIfNotExists(string setCode, string description)
    {
        var cardSet = await QueryAsSingle(entities => entities.Where(s => s.SetCode == setCode));
        if (cardSet == null)
        {
            cardSet = new CardSetEntity
            {
                SetCode = setCode,
                Description = description
            };

            return await Insert(cardSet);
        }

        return cardSet;
    }

    public  Task<CardSetEntity> FindById(string setId)
    {
        return QueryAsSingle(entities => entities.Where(s => s.SetCode == setId));
    }
}