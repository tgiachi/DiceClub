using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Database.Entities.Inventory;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services
{
    public class BookService : AbstractBaseService<BookService>
    {

        private readonly InventoryService _inventoryService;

        public BookService(IEventBusService eventBusService, ILogger<BookService> logger, InventoryService inventoryService) : base(eventBusService, logger)
        {
            _inventoryService = inventoryService;
        }

        public async Task<Volume> SearchBookByIsbn(string isbn)
        {
            using var bookClient = new BooksService();

            var results = await bookClient.Volumes.List($"isbn:{isbn}").ExecuteAsync();

            if (results.Items.Any())
            {
                return results.Items.First();
            }

            return null;

        }
    }
}
