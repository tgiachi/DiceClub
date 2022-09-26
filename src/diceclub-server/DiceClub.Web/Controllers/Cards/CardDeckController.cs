using DiceClub.Api.Data.Rest;
using DiceClub.Services.Base;
using DiceClub.Services.Cards;
using DiceClub.Services.Data.Cards.Deck;
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

    public CardDeckController(CardDeckService cardDeckService)
    {
        _cardDeckService = cardDeckService;
    }

    [HttpGet]
    [Route("preset/mana_curves")]
    public RestResultObject<List<DeckManaCurvePreset>> GetDefaultCurves()
    {
        return RestResultObjectBuilder<List<DeckManaCurvePreset>>.Create().Data(_cardDeckService.GetManaCurvePresets())
            .Build();
    }


    [HttpPost]
    [Route("random/deck")]
    public async Task<RestResultObject<bool>> CreateRandomDeck([FromBody] DeckCreateRequest request)
    {
        await _cardDeckService.CreateRandomDeck(request, GetUserId());

        return new RestResultObjectBuilder<bool>().Data(true).Build();
    }
}