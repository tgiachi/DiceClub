using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards
{

    [DataAccess]
    public class CardsDao : AbstractDataAccess<Guid, CardEntity, DiceClubDbContext>
    {
        public CardsDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<CardEntity> logger) : base(dbContext, logger)
        {
        }

        public async Task<bool> CheckIfCardExists(string name)
        {
            var card = await QueryAsSingle(entities => entities.Where(s => s.CardName == name));

            return card != null;
        }

        public async Task<bool> IncrementQuantity(string cardName)
        {
            var card = await QueryAsSingle(entities => entities.Where(s => s.CardName == cardName));

            card.Quantity += 1;

            await Update(card);

            return true;
        }
    }
}
