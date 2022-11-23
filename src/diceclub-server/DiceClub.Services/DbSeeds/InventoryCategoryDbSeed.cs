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
using DiceClub.Services.Parsers;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.DbSeeds
{

    [DbSeed(3)]
    public class InventoryCategoryDbSeed : AbstractDbSeed<Guid, InventoryCategoryEntity>
    {

        private readonly List<string> _categories = new List<string>() { "BOOKS", "BOARD_GAMES", "COMICS" };
        private readonly List<string> _categoriesTypes = new List<string>() { typeof(BookCategoryParser).FullName, typeof(BoardGameCategoryParser).FullName, typeof(ComicCategoryParser).FullName };

        public InventoryCategoryDbSeed(InventoryCategoryDao dao, ILogger<AbstractDbSeed<Guid, InventoryCategoryEntity>> logger) : base(dao, logger)
        {
        }

        public override async Task<bool> Seed()
        {
            var index = 0;
            foreach (var category in _categories)
            {
                var exists = await Dao.QueryAsSingle(entities => entities.Where(s => s.Name == category));

                if (exists == null)
                {
                    await Dao.Insert(new InventoryCategoryEntity
                    {
                        Name = category,
                        ParserClassType = _categoriesTypes[index]
                    });

                }
                else
                {
                    exists.ParserClassType = _categoriesTypes[index];
                    await Dao.Update(exists);
                }

                index++;
            }


            return true;
        }
    }
}
