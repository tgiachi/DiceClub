using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Database.Dao;
using DiceClub.Database.Entities.Inventory;
using Google.Apis.Books.v1.Data;
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

            return await _inventoryDao.Insert(new Inventory
            {
                CategoryId = category.Id,
                OwnerId = userId,
                Description = volume.VolumeInfo.Description,
                Name = volume.VolumeInfo.Title,
                SerialNumber = volume.VolumeInfo.IndustryIdentifiers[1].Identifier,
                Image = volume.VolumeInfo.ImageLinks.Thumbnail
            });

        }
    }
}
