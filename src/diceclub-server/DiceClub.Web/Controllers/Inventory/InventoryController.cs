using DiceClub.Services.Base;
using DiceClub.Services.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Inventory
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/inventory")]
    [Authorize]
    public class InventoryController : BaseAuthController
    {

        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        [Route("search/isbn")]
        public async Task<bool> SearchAndAddInventory(string categoryName, string ean)
        {
            await _inventoryService.SearchIsbnForCategory(categoryName, ean);

            return true;
        }
    }
}
