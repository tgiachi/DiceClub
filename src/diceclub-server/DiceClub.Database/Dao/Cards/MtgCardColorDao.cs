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
    public class MtgCardColorDao : AbstractDataAccess<Guid, MtgCardColorEntity, DiceClubDbContext>
    {
        public MtgCardColorDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgCardColorEntity> logger) : base(dbContext, logger)
        {
        }

        public async Task<MtgCardColorEntity> InsertIfNotExists(string symbol, string description, string image)
        {
            var exists = await QueryAsSingle(entities => entities.Where(s => s.Name == symbol));

            if (exists != null)
            {
                return exists;
            }

            exists = await Insert(new MtgCardColorEntity()
            {
                Description = description,
                ImageUrl = image,
                Name = symbol
            });

            return exists;
        }


    }
}
