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
    public class MtgCardSetDao : AbstractDataAccess<Guid, MtgCardSetEntity, DiceClubDbContext>
    {
        public MtgCardSetDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgCardSetEntity> logger) : base(dbContext, logger)
        {
        }

        public async Task<MtgCardSetEntity> InsertIfNotExists(string code, string description, string image, int cardCount)
        {
            var exists = await QueryAsSingle(entities =>
                entities.Where(s => s.Code.ToLower() == code.ToLower()));

            if (exists != null)
            {
                return exists;
            }

            exists = await Insert(new MtgCardSetEntity
            {
                Code = code,
                Description = description,
                Image = image,
                CardCount = cardCount
            });

            return exists;

        }
    }
}
