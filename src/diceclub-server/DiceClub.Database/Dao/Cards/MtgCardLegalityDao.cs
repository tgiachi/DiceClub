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
    public class MtgCardLegalityDao : AbstractDataAccess<Guid, MtgCardLegalityEntity, DiceClubDbContext>
    {
        public MtgCardLegalityDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgCardLegalityEntity> logger) : base(dbContext, logger)
        {
        }

        public async Task<MtgCardLegalityEntity> InsertIfNotExists(string name)
        {
            var exists = await QueryAsSingle(entities => entities.Where(s => s.Name == name));
            if (exists != null)
            {
                return exists;
            }

            exists = await Insert(new MtgCardLegalityEntity
            {
                Name = name
            });

            return exists;
        }
    }
}
