using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dao;
using DiceClub.Database.Context;
using DiceClub.Database.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Dao.Inventory
{
    [DataAccess]
    public class InventoryCategoryDao : AbstractDataAccess<Guid, InventoryCategoryEntity, DiceClubDbContext>
    {
        public InventoryCategoryDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<InventoryCategoryEntity> logger) : base(dbContext, logger)
        {
        }
    }
}
