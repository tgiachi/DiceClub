using Aurora.Turbine.Api.Data.Pagination;
using Aurora.Turbine.Api.Interfaces;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Dto.Cards.Mappers;
using DiceClub.Database.Entities.Cards;
using DiceClub.Services.Cards;
using DiceClub.Services.Data.Card;
using DiceClub.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Cards;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
// [Authorize]
public class CardController : BaseAuthController
{
    private readonly ImportService _importService;
    private readonly ImportMtgService _importMtgService;
    private readonly CardService _cardService;
    private readonly IRestPaginatorService _restPaginatorService;
    private readonly CardDtoMapper _cardDtoMapper;
    

    public CardController(ImportService importService, ImportMtgService importMtgService, CardService cardService, IRestPaginatorService restPaginatorService, CardDtoMapper cardDtoMapper)
    {
        _importService = importService;
        _importMtgService = importMtgService;
        _cardService = cardService;
        _restPaginatorService = restPaginatorService;
        _cardDtoMapper = cardDtoMapper;
    }
    
    [HttpPost]
    [Route("upload/format/cardcastle")]
    public async Task<IActionResult> UploadCardCastleCsv(IFormFile file)
    {
        using var ms = new MemoryStream();
        var tmpFile = Path.Join(Path.GetTempPath(), file.FileName);
        await file.CopyToAsync(ms);
        await System.IO.File.WriteAllBytesAsync(tmpFile, ms.ToArray());
        await _importService.ImportCardCastleCsv(tmpFile, GetUserId());
        return Ok();
    }

    [HttpPost]
    [Route("import/mtg")]
    public async Task<IActionResult> ImportMtgDatabase()
    {
        await Task.Run(() => _importMtgService.ImportMtgDatabase());
        return Ok();
    }

    
    [HttpPost]
    [Route("search/")]
    public async Task<PaginationObject<CardDto>> Search([FromBody] CardQueryObject query, [FromQuery] int pageNum = 1, [FromQuery] int pageSize = 50)
    {
        var result = await _cardService.SearchCards(query);

        return await _restPaginatorService.Paginate<Guid, CardEntity, CardDto, CardDtoMapper>(result, pageNum,
            pageSize, _cardDtoMapper);

    }

}