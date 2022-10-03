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
    public class MtgCardLanguageDao  : AbstractDataAccess<Guid,MtgCardLanguageEntity, DiceClubDbContext>
    {
        public MtgCardLanguageDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgCardLanguageEntity> logger) : base(dbContext, logger)
        {
        }

        public async Task<MtgCardLanguageEntity> InsertIfNotExists(string name, string code)
        {
            var exists = await QueryAsSingle(entities => entities.Where(s => s.Name == name));
            if (exists != null)
            {
                return exists;
            }

            exists = await Insert(new MtgCardLanguageEntity
            {
                Name = name,
                Code = code,
            });

            return exists;
        }
    }
}
