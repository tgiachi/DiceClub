using DiceClub.Api.Data.Cards;
using DiceClub.Api.Data.Rest;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Dto.Mappers.Cards;
using DiceClub.Database.Entities.MtgCards;
using DiceClub.Services.Base;
using DiceClub.Services.Cards;
using DiceClub.Services.Paginator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Cards;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[Authorize]
public class CardsController : BaseAuthController
{
    private readonly MtgCardMapper _mtgCardMapper;
    private readonly CardService _cardService;
    private readonly RestPaginatorService _restPaginatorService;

    public CardsController(MtgCardMapper mtgCardMapper, CardService cardService,
        RestPaginatorService restPaginatorService)
    {
        _mtgCardMapper = mtgCardMapper;
        _cardService = cardService;
        _restPaginatorService = restPaginatorService;
    }

    [HttpPost]
    [Route("search")]
    public async Task<ActionResult<PaginatedRestResultObject<MtgCardDto>>> Search([FromBody] SearchCardRequest query,
        int page = 1, int pageSize = 30)
    {
        var result = await _cardService.SearchCards(query, GetUserId(), page, pageSize);

        return PaginatedRestResultObjectBuilder<MtgCardDto>.Create()
            .Data(_mtgCardMapper.ToDto(result.Cards))
            .Page(page)
            .PageCount((int)Math.Ceiling((double)result.TotalCount / pageSize))
            .PageSize(pageSize)
            .Total(result.TotalCount)
            .Build();
    }

    [HttpPost]
    [Route("add/{id}")]
    public async Task<ActionResult<bool>> AddCardById(string id)
    {
        await _cardService.AddCardById(id, GetUserId());

        return Ok(true);
    }
}