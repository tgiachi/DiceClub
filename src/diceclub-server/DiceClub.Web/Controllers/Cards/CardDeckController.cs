using DiceClub.Api.Data.Rest;
using DiceClub.Database.Dto.Cards.Deck;
using DiceClub.Database.Dto.Mappers.Deck;
using DiceClub.Database.Entities.Deck;
using DiceClub.Services.Base;
using DiceClub.Services.Cards;
using DiceClub.Services.Data.Cards.Deck;
using DiceClub.Services.Paginator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Cards;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/cards/deck")]
[Authorize]
public class CardDeckController : BaseAuthController
{
    private readonly CardDeckService _cardDeckService;
    private readonly RestPaginatorService _restPaginatorService;
    private readonly DeckMasterMapper _deckMasterMapper;
    private readonly DeckDetailMapper _deckDetailMapper;

    public CardDeckController(CardDeckService cardDeckService, RestPaginatorService restPaginatorService,
        DeckMasterMapper deckMasterMapper, DeckDetailMapper deckDetailMapper)
    {
        _cardDeckService = cardDeckService;
        _restPaginatorService = restPaginatorService;
        _deckMasterMapper = deckMasterMapper;
        _deckDetailMapper = deckDetailMapper;
    }

    [HttpGet]
    [Route("preset/mana_curves")]
    public RestResultObject<List<DeckManaCurvePreset>> GetDefaultCurves()
    {
        return RestResultObjectBuilder<List<DeckManaCurvePreset>>.Create().Data(_cardDeckService.GetManaCurvePresets())
            .Build();
    }


    [HttpPost]
    [Route("random/single/deck")]
    public async Task<RestResultObject<bool>> CreateRandomDeck([FromBody] DeckCreateRequest request)
    {
        await _cardDeckService.CreateRandomDeck(request, GetUserId());

        return new RestResultObjectBuilder<bool>().Data(true).Build();
    }

    [HttpPost]
    [Route("random/multiple/deck")]
    public RestResultObject<bool> CreateMultipleRandomDecks([FromBody] DeckMultipleDeckRequest request)
    {
        Task.Run(async () => { await _cardDeckService.CreateMultipleRandomDeck(request, GetUserId()); });
        return RestResultObjectBuilder<bool>.Create().Data(true).Build();
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedRestResultObject<DeckMasterDto>>> GetDeckMasters(int page = 1,
        int pageSize = 20)
    {
        return Ok(await _restPaginatorService.Paginate<Guid, DeckMasterEntity, DeckMasterDto, DeckMasterMapper>(
            await _cardDeckService.FindDeckMasterByUserId(GetUserId()), page, pageSize, _deckMasterMapper));
    }


    [HttpGet]
    [Route("master/{id:guid}")]
    public async Task<RestResultObject<List<DeckDetailDto>>> GetDeckDetailById(Guid id)
    {
        var results = await _cardDeckService.FindDeckDetailById(id, GetUserId());

        return RestResultObjectBuilder<List<DeckDetailDto>>.Create().Data(_deckDetailMapper.ToDto(results)).Build();
    }
}