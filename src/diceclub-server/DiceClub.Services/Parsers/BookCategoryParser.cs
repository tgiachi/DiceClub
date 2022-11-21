using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Attributes.Inventory;
using DiceClub.Api.Data.Inventory;
using DiceClub.Api.Interfaces;
using Google.Apis.Books.v1;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Parsers
{
    [InventoryHandler]
    public class BookCategoryParser : AbstractBaseService<BookCategoryParser>, IInventoryCategoryParser
    {
        public async Task<InventoryCategorySearchResult> Search(string ean)
        {
            using var bookService = new BooksService();

            var result = await bookService.Volumes.List(ean).ExecuteAsync();
            if (result.Items.Count > 0)
            {
                return new InventoryCategorySearchResult
                {
                    Title = result.Items[0].VolumeInfo.Title,
                    Description = result.Items[0].VolumeInfo.Description

                };
            }

            return null;
        }

        public BookCategoryParser(IEventBusService eventBusService, ILogger<BookCategoryParser> logger) : base(eventBusService, logger)
        {

        }
    }
}
