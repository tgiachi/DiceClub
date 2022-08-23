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

namespace DiceClub.Database.Dao
{

    [DataAccess]
    public class InventoryCategoryDao : AbstractDataAccess<Guid, InventoryCategory, DiceClubDbContext>
    {
        public InventoryCategoryDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<InventoryCategory> logger) : base(dbContext, logger)
        {

        }

        public Task<InventoryCategory> FindByName(string name)
        {
            return QueryAsSingle(queryable => queryable.Where(s => s.Name == name));
        }
    }
}
