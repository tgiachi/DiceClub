using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards;


[DataAccess]
public class CardStagingDao : AbstractDataAccess<Guid, CardStagingEntity, DiceClubDbContext>
{
    public CardStagingDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CardStagingEntity> logger) : base(dbContext, logger)
    {
    }

    public Task<List<CardStagingEntity>> FindStagingCardByUser(Guid userId)
    {
        return QueryAsList(entities => entities.Where(s => s.UserId == userId && !s.IsAdded));
    }

    public Task<bool> DeleteStagingCard(Guid id)
    {
        return Delete(id);
    }
}