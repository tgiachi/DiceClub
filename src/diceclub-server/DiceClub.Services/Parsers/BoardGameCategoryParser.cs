using Aurora.Api.Services.Base;
using DiceClub.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using DiceClub.Api.Attributes.Inventory;
using DiceClub.Api.Data.Inventory;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Parsers
{

    [InventoryHandler]
    public class BoardGameCategoryParser : AbstractBaseService<BoardGameCategoryParser>, IInventoryCategoryParser
    {
        public BoardGameCategoryParser(IEventBusService eventBusService, ILogger<BoardGameCategoryParser> logger) : base(eventBusService, logger)
        {
        }

        public Task<InventoryCategorySearchResult> Search(string ean)
        {
            throw new NotImplementedException();
        }
    }
}
