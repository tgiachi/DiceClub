using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards;


[DataAccess]
public class CardCardLegalityDao : AbstractDataAccess<Guid, CardCardLegality, DiceClubDbContext>
{
    public CardCardLegalityDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CardCardLegality> logger) : base(dbContext, logger)
    {
    }
}