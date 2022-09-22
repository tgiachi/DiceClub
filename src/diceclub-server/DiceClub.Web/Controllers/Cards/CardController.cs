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
public class CardController : BaseAuthController
{
    private readonly MtgCardMapper _mtgCardMapper;
    private readonly CardService _cardService;
    private readonly RestPaginatorService _restPaginatorService;

    public CardController(MtgCardMapper mtgCardMapper, CardService cardService,
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
        var result = await _cardService.SearchCards(query, GetUserId());

        return await _restPaginatorService.Paginate<Guid, MtgCardEntity, MtgCardDto, MtgCardMapper>(result, page,
            pageSize, _mtgCardMapper);
    }
}