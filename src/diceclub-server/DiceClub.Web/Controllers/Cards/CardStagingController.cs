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
[Route("api/v1/cards/staging")]
[Authorize]
public class CardStagingController : BaseAuthController
{
    private readonly CardStageService _cardStageService;
    private readonly RestPaginatorService _restPaginatorService;
    private readonly MtgCardStageMapper _mtgCardStageMapper;

    public CardStagingController(CardStageService cardStageService, RestPaginatorService restPaginatorService, MtgCardStageMapper mtgCardStageMapper)
    {
        _cardStageService = cardStageService;
        _restPaginatorService = restPaginatorService;
        _mtgCardStageMapper = mtgCardStageMapper;
    }

    [Route("add")]
    [HttpPost]
    public async Task<ActionResult<RestResultObject<bool>>> AddCard([FromBody] CardStageAddRequest request)
    {
        await _cardStageService.AddCardInStaging(request, GetUserId());

        return Ok(RestResultObjectBuilder<bool>.Create().Data(true).Build());
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedRestResultObject<MtgCardStageDto>>> FindAll(int page = 1, int pageSize = 30)
    {
        return Ok(await _restPaginatorService.Paginate<Guid, MtgCardStageEntity, MtgCardStageDto, MtgCardStageMapper>(
            await _cardStageService.FindAllByUser(GetUserId()), page, pageSize, _mtgCardStageMapper));
    }
}