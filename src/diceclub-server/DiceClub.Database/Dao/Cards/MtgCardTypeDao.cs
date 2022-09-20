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
    public class MtgCardTypeDao : AbstractDataAccess<Guid, MtgCardTypeEntity, DiceClubDbContext>
    {
        public MtgCardTypeDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgCardTypeEntity> logger) : base(dbContext, logger)
        {
        }
    }
}
