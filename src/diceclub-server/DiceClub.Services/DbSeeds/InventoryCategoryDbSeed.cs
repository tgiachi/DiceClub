using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Attributes;
using Aurora.Api.Entities.Impl.Seeds;
using Aurora.Api.Entities.Interfaces.Dao;
using DiceClub.Database.Dao.Inventory;
using DiceClub.Database.Entities.Inventory;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.DbSeeds
{

    [DbSeed(3)]
    public class InventoryCategoryDbSeed : AbstractDbSeed<Guid, InventoryCategoryEntity>
    {

        private readonly List<string> _categories = new List<string>() { "BOOKS", "BOARD_GAMES" };

        public InventoryCategoryDbSeed(InventoryCategoryDao dao, ILogger<AbstractDbSeed<Guid, InventoryCategoryEntity>> logger) : base(dao, logger)
        {
        }

        public override async Task<bool> Seed()
        {
            foreach (var category in _categories)
            {
                var exists = Dao.QueryAsSingle(entities => entities.Where(s => s.Name == category));

                if (exists == null)
                {
                    await Dao.Insert(new InventoryCategoryEntity
                    {
                        Name = category
                    });

                }
            }


            return true;
        }
    }
}
