using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.MtgCards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards;


[DataAccess]
public class MtgCardSymbolDao : AbstractDataAccess<Guid, MtgCardSymbolEntity, DiceClubDbContext>
{
    public MtgCardSymbolDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgCardSymbolEntity> logger) : base(dbContext, logger)
    {
    }

    public async Task<MtgCardSymbolEntity> CreateIfNotExists(string symbol, string image, string description)
    {
        var entity = await QueryAsSingle(entities => entities.Where(s => s.Symbol == symbol));
        if (entity != null)
        {
            return entity;
        }

        return await Insert( new MtgCardSymbolEntity
        {
            Description = description,
            Image = image,
            Symbol = symbol
        });
    }
}