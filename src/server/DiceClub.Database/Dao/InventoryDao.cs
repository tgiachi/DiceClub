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
    public class InventoryDao : AbstractDataAccess<Guid, Inventory, DiceClubDbContext>
    {
        public InventoryDao(IDbContextFactory<DiceClubDbContext> dbContext, ILogger<Inventory> logger) : base(dbContext, logger)
        {

        }

        public Task<Inventory> ByIdWithCategory(Guid id)
        {
            return QueryAsSingle(queryable => queryable.Where(s => s.Id == id).Include(k => k.Category));
        }
    }
}
