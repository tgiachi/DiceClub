using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Attributes;
using Aurora.Api.Entities.Impl.Seeds;
using Aurora.Api.Entities.Interfaces.Dao;
using DiceClub.Database.Dao;
using DiceClub.Database.Entities.Account;
using DiceClub.Database.Entities.Inventory;
using Microsoft.Extensions.Logging;

namespace DiceClub.Database.Seeds
{

    [DbSeed(1)]
    public class InventorySeed : AbstractDbSeed<Guid, InventoryCategory>
    {
        private readonly List<string> _defaultCategories = new List<string>() { "BOOKS", "BOARD_GAMES" };

        public InventorySeed(InventoryCategoryDao dao, ILogger<AbstractDbSeed<Guid, InventoryCategory>> logger) : base(dao, logger)
        {

        }

        public async override Task<bool> Seed()
        {
            foreach (var category in _defaultCategories)
            {
                var cat = await Dao.QueryAsSingle(queryable => queryable.Where(s => s.Name == category));

                if (cat == null)
                {
                    await Dao.Insert(new InventoryCategory
                    {
                        Name = category
                    });
                }
            }

            return true;
        }
    }
}
