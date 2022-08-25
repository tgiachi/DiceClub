using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Events.Inventory;
using DiceClub.Database.Dao;
using DiceClub.Database.Entities.Inventory;
using Google.Apis.Books.v1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services
{
    public class InventoryService : AbstractBaseService<InventoryService>
    {

        private readonly InventoryCategoryDao _inventoryCategoryDao;
        private readonly InventoryDao _inventoryDao;

        public InventoryService(IEventBusService eventBusService, ILogger<InventoryService> logger, InventoryCategoryDao inventoryCategoryDao, InventoryDao inventoryDao) : base(eventBusService, logger)
        {

            _inventoryCategoryDao = inventoryCategoryDao;
            _inventoryDao = inventoryDao;
        }

        public async Task<Inventory> InsertBook(Volume volume, Guid userId)
        {
            var category = await _inventoryCategoryDao.FindByName("BOOKS");

            return await AddInventory(new Inventory
            {
                Author = string.Join(',', volume.VolumeInfo.Authors),
                CategoryId = category.Id,
                OwnerId = userId,
                Description = volume.VolumeInfo.Description ?? "N/A",
                Name = volume.VolumeInfo.Title,
                SerialNumber = volume.VolumeInfo.IndustryIdentifiers[1].Identifier,
                Image = volume.VolumeInfo.ImageLinks.Thumbnail
            });

        }

        public async Task<Inventory> AddInventory(Inventory inventory)
        {
            var insertedInventory = await _inventoryDao.Insert(inventory);

            await PublishEvent(new InventoryItemAddedEvent() { Id = insertedInventory.Id });
            return insertedInventory;
        }

        public Task<List<Inventory>> FindAllQuery()
        {
            return _inventoryDao.QueryAsList(queryable => queryable.Include(k => k.Category));
        }

        public Task<Inventory> FindById(Guid id)
        {
            return _inventoryDao.FindById(id);
        }
    }
}
