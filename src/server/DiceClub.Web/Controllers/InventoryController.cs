using DiceClub.Database.Dto.Mappers;
using DiceClub.Services;
using DiceClub.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers
{


    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class InventoryController : BaseAuthController
    {
        private readonly BookService _bookService;
        private readonly InventoryService _inventoryService;
        private readonly InventoryDtoMapper _inventoryDtoMapper;

        public InventoryController(BookService bookService, InventoryService inventoryService, InventoryDtoMapper inventoryDtoMapper)
        {
            _bookService = bookService;
            _inventoryService = inventoryService;
            _inventoryDtoMapper = inventoryDtoMapper;
        }


        [HttpPost]
        [Route("add/book/{isbn}")]
        public async Task<IActionResult> AddBook(string isbn)
        {
            var volume = await _bookService.SearchBookByIsbn(isbn);
            if (volume == null)
            {
                return BadRequest("ISBN not found");
            }

            var inventory = await _inventoryService.InsertBook(volume, GetUserId());

            return Ok(_inventoryDtoMapper.ToDto(inventory));
        }
    }
}
