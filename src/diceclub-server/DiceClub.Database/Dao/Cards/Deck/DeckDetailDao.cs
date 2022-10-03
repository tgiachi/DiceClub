using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Deck;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards.Deck;


[DataAccess]
public class DeckDetailDao : AbstractDataAccess<Guid, DeckDetailEntity, DiceClubDbContext>
{
    public DeckDetailDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<DeckDetailEntity> logger) : base(dbContext, logger)
    {
    }
}