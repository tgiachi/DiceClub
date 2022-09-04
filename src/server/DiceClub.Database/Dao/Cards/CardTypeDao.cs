using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtg.Collection.Manager.Database.Entities;

namespace DiceClub.Database.Dao.Cards
{
    [DataAccess]

    public class CardTypeDao : AbstractDataAccess<Guid, CardTypeEntity, DiceClubDbContext>
    {
        public CardTypeDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CardTypeEntity> logger) : base(dbContext, logger)
        {
        }

        public async Task<CardTypeEntity> AddCardTypeIfNotExists(string type)
        {
            var exists = await QueryAsSingle(entities => entities.Where(s => s.CardType == type));
            if (exists == null)
            {
                exists = await Insert(new CardTypeEntity
                {
                    CardType = type
                });
            }

            return exists;
        }
    }
}
