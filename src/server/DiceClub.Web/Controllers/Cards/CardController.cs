using Aurora.Turbine.Api.Data.Pagination;
using Aurora.Turbine.Api.Interfaces;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Dto.Cards.Mappers;
using DiceClub.Database.Entities.Cards;
using DiceClub.Services.Cards;
using DiceClub.Services.Data.Card;
using DiceClub.Web.Controllers.Base;
using DiceClub.Web.Data.Rest;
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
    private readonly CardSetDtoMapper _cardSetDtoMapper;
    private readonly CardLegalityDtoMapper _cardLegalityDtoMapper;
    private readonly CardLegalityTypeMapper _cardLegalityTypeMapper;
    private readonly CardCreatureTypeDtoMapper _cardCreatureTypeDtoMapper;
    private readonly CardTypeDtoMapper _cardTypeDtoMapper;
    private readonly MtgDao _mtgDao;
    private readonly CardStagingService _cardStagingService;


    public CardController(ImportService importService, ImportMtgService importMtgService, CardService cardService,
        IRestPaginatorService restPaginatorService, CardDtoMapper cardDtoMapper, CardSetDtoMapper cardSetDtoMapper,
        CardLegalityDtoMapper cardLegalityDtoMapper, CardLegalityTypeMapper cardLegalityTypeMapper,
        CardCreatureTypeDtoMapper cardCreatureTypeDtoMapper, CardTypeDtoMapper cardTypeDtoMapper,
        MtgDao mtgDao, CardStagingService cardStagingService)
    {
        _importService = importService;
        _importMtgService = importMtgService;
        _cardService = cardService;
        _restPaginatorService = restPaginatorService;
        _cardDtoMapper = cardDtoMapper;
        _cardSetDtoMapper = cardSetDtoMapper;
        _cardLegalityDtoMapper = cardLegalityDtoMapper;
        _cardLegalityTypeMapper = cardLegalityTypeMapper;
        _cardCreatureTypeDtoMapper = cardCreatureTypeDtoMapper;
        _cardTypeDtoMapper = cardTypeDtoMapper;

        _mtgDao = mtgDao;
        _cardStagingService = cardStagingService;
    }

    [HttpGet]
    [Route("sets/")]
    public async Task<PaginationObject<CardSetDto>> GetAllSets(int page = 1, int pageSize = 30)
    {
        return await _restPaginatorService.Paginate<Guid, CardSetEntity, CardSetDto, CardSetDtoMapper>(
            await _cardService.FindAllSets(), page, pageSize, _cardSetDtoMapper);
    }

    [HttpGet]
    [Route("creatures/types")]
    public async Task<List<CreatureTypeDto>> GetCreatureTypes()
    {
        return _cardCreatureTypeDtoMapper.ToDto(await _cardService.FindAllCreatureTypes());
    }

    [HttpGet]
    [Route("types")]
    public async Task<List<CardTypeDto>> GetCardTypes()
    {
        return _cardTypeDtoMapper.ToDto(await _cardService.FindAllCardTypes());
    }


    [HttpGet]
    [Route("legalities")]
    public async Task<List<CardLegalityDto>> GetAllCartLegalities()
    {
        return _cardLegalityDtoMapper.ToDto(await _cardService.FindAllLegalities());
    }

    [HttpGet]
    [Route("legalities/types")]
    public async Task<List<CardLegalityTypeDto>> GetAllCartLegalitiesType()
    {
        return _cardLegalityTypeMapper.ToDto(await _cardService.FindAllLegalityTypes());
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
    public async Task<PaginationObject<CardDto>> Search([FromBody] CardQueryObject query, [FromQuery] int pageNum = 1,
        [FromQuery] int pageSize = 50)
    {
        var result = await _cardService.SearchCards(query);

        return await _restPaginatorService.Paginate<Guid, CardEntity, CardDto, CardDtoMapper>(result, pageNum,
            pageSize, _cardDtoMapper);
    }


    [HttpGet]
    [Route("ml/prepare/data")]
    public async Task PrepareImagesForMl()
    {
        await _importMtgService.PrepareDataForMachineLearning();
    }


    [HttpGet]
    [Route("staging/autocomplete")]
    public async Task<SearchCardAutoCompleteResponse> CardAutoComplete([FromQuery] string cardName,
        [FromQuery] string? setCode, [FromQuery] int limit = 10)
    {
        var results = await _cardService.SearchAutoComplete(cardName, setCode);

        var cardsResponse = new SearchCardAutoCompleteResponse()
        {
            SearchText = cardName,
            AutoCompleteCards = new()
        };

        if (results.Count == 0)
        {
            return cardsResponse;
        }

        var limitedCards = results.Take(limit).ToList();

        foreach (var card in limitedCards)
        {
            cardsResponse.AutoCompleteCards.Add(new SearchAutoCompleteCard
            {
                MtgId = card.MultiverseId ?? 0,
                Name = card.CardName,
                ImageUrl = card.ImageUrl,
                SetCode = card.SetCode.ToLower()
            });
        }

        return cardsResponse;
    }


    [HttpGet]
    [Route("staging/view")]
    public async Task<List<StagingCardResponse>> ViewStagingCards()
    {
        var cards = await _cardStagingService.FindStagingCardByUser(GetUserId());
        var mtgEntities = await _mtgDao.FindByListOfIds(cards.Select(s => s.MtgId).ToList());
        var results = new List<StagingCardResponse>();

        foreach (var card in cards)
        {
            var mtgEntity = mtgEntities.FirstOrDefault(s => s.MultiverseId == card.MtgId);
            results.Add(new StagingCardResponse
            {
                Id = card.Id,
                Name = mtgEntity.CardName,
                ImageUrl = mtgEntity.ImageUrl ?? "",
                MtgId = card.MtgId,
                SetCode = mtgEntity.SetCode
            });
        }

        return results;
    }


    [HttpPost]
    [Route("staging/add")]
    public Task<Guid> AddCardToStaging([FromBody] StagingCardRequest request)
    {
        return _cardStagingService.AddCardToStaging(request.MtgId, request.Language, request.IsFoil, GetUserId());
    }


    [HttpDelete]
    [Route("staging/{id:guid}")]
    public Task<bool> DeleteStagingCard(Guid id)
    {
        return _cardStagingService.DeleteStagingCard(id);
    }

    [HttpPost]
    [Route("staging/commit/all")]
    public Task<List<bool>> CommitAllStagingCards()
    {
        return _cardStagingService.CommitAllCards(GetUserId());
    }

    [HttpPost]
    [Route("staging/commit/{id}")]
    public Task<bool> CommitStagingCard(Guid id)
    {
        return _cardStagingService.CommitCardStaging(id);
    }
}