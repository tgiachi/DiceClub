using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.MtgCards;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Cards
{

    [DataAccess]
    public class MtgCardDao : AbstractDataAccess<Guid, MtgCardEntity, DiceClubDbContext>
    {
        public MtgCardDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgCardEntity> logger) : base(dbContext, logger)
        {
        }

        public async Task<bool> CheckCardExistsAndIncQuantity(string cardName, string setCode, Guid user, int incQuantity = 1)
        {
            var card = await QueryAsSingle(entities => entities
                .Where(s => s.Name == cardName && s.Set.Code == setCode && s.OwnerId == user)
                .Include(s => s.Set));

            if (card != null)
            {
                card.Quantity = card.Quantity + incQuantity;
                await Update(card);
                return true;
            }

            return false;

        }
    }
}
