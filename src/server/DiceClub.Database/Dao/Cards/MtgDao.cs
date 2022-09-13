using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Api.Data.Cards.Mtg;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards;


[DataAccess]
public class MtgDao : AbstractDataAccess<Guid, MtgEntity, DiceClubDbContext>
{
    public MtgDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgEntity> logger) : base(dbContext, logger)
    {
    }

    public Task<MtgEntity> FindByCard(MtgCard card)
    {
        return QueryAsSingle(entities => entities.Where(s => s.Card == card));
    }

    public Task<MtgEntity> FindByIdAndName(int? multiverseId, string name)
    {
        return QueryAsSingle(entities => entities.Where(s => s.MultiverseId == multiverseId && s.CardName == name));
    }

    public Task<MtgEntity> FindByMultiverseId(int multiverseId)
    {
        return QueryAsSingle(entities => entities.Where(s => s.MultiverseId == multiverseId));
    }

    public Task<MtgEntity> FindByName(string name)
    {
        return QueryAsSingle(entities => entities.Where(s => s.CardName == name));
    }
}