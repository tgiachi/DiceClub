using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using Aurora.Api.Utils;
using Autofac;
using Autofac.Core.Lifetime;
using DiceClub.Api.Interfaces;
using DiceClub.Database.Dao.Inventory;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Inventory
{
    public class InventoryService : AbstractBaseService<InventoryService>
    {
        private readonly InventoryCategoryDao _inventoryCategoryDao;
        private readonly ILifetimeScope _lifetimeScope;

        public InventoryService(IEventBusService eventBusService, ILogger<InventoryService> logger, InventoryCategoryDao inventoryCategoryDao, ILifetimeScope lifetimeScope) : base(eventBusService, logger)
        {
            _inventoryCategoryDao = inventoryCategoryDao;
            _lifetimeScope = lifetimeScope;
        }

        public async Task SearchIsbnForCategory(string categoryName, string ean)
        {
            var category =
                await _inventoryCategoryDao.QueryAsSingle(entities => entities.Where(k => k.Name == categoryName));

            var parser =
                _lifetimeScope.BeginLifetimeScope().Resolve(AssemblyUtils.GetType(category.ParserClassType)) as
                    IInventoryCategoryParser;

            var entity = await parser.Search(ean);


        }

    }
}
