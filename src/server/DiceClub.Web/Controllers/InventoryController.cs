using Aurora.Turbine.Api.Data.Pagination;
using Aurora.Turbine.Api.Interfaces;
using DiceClub.Database.Dto;
using DiceClub.Database.Dto.Mappers;
using DiceClub.Database.Entities.Inventory;
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
        private readonly IRestPaginatorService _restPaginatorService;

        public InventoryController(BookService bookService, InventoryService inventoryService, InventoryDtoMapper inventoryDtoMapper, IRestPaginatorService paginatorService)
        {
            _bookService = bookService;
            _inventoryService = inventoryService;
            _inventoryDtoMapper = inventoryDtoMapper;
            _restPaginatorService = paginatorService;
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

        [HttpGet]
        [Route("get/all")]
        public async Task<PaginationObject<InventoryDto>> FindAllInventory(int pageNumber = 1, int pageSize = 30)
        {
            var inventory = await _inventoryService.FindAllQuery();
            return await _restPaginatorService.Paginate<Guid, Inventory, InventoryDto, InventoryDtoMapper>(inventory, pageNumber, pageSize,
                  _inventoryDtoMapper);
        }

        [HttpGet]
        [Route("get/{id:guid}")]
        public async Task<ActionResult<InventoryDto>> FindById(Guid id)
        {
            var inventory = await _inventoryService.FindById(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(_inventoryDtoMapper.ToDto(inventory));

        }
    }
}
