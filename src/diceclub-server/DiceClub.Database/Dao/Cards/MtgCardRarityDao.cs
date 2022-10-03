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
    public class MtgCardRarityDao : AbstractDataAccess<Guid, MtgCardRarityEntity, DiceClubDbContext>
    {
        public MtgCardRarityDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgCardRarityEntity> logger) :
            base(dbContext, logger)
        {
        }

        public  Task<MtgCardRarityEntity> FindByCode(string name)
        {
            return QueryAsSingle(entities => entities.Where(s => s.Name == name));
        }

        public async Task<MtgCardRarityEntity> AddIfNotExists(string name)
        {
            var exists = await QueryAsSingle(entities => entities.Where(s => s.Name.ToLower() == name.ToLower()));

            if (exists != null)
            {
                return exists;
            }

            return await Insert(new MtgCardRarityEntity
            {
                Name = name
            });
        }
    }
}