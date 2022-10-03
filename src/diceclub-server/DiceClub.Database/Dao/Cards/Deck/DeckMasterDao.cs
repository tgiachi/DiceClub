using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Deck;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards.Deck;


[DataAccess]
public class DeckMasterDao : AbstractDataAccess<Guid, DeckMasterEntity, DiceClubDbContext>
{
    public DeckMasterDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<DeckMasterEntity> logger) : base(dbContext, logger)
    {
    }
}