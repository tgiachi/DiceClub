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
    public class MtgDumpDao : AbstractDataAccess<Guid, MtgDumpEntity, DiceClubDbContext>
    {
        public MtgDumpDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<MtgDumpEntity> logger) : base(dbContext, logger)
        {
        }
    }
}
