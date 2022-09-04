using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dao.Cards
{
    [DataAccess]

    public class ColorCardDao : AbstractDataAccess<Guid, ColorCardEntity, DiceClubDbContext>
    {
        public ColorCardDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<ColorCardEntity> logger) : base(dbContext, logger)
        {
        }
    }
}
